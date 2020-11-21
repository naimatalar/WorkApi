using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Work.Infrastructure;
using Work.Infrastructure.DependencyInjection;
using Work.Infrastructure.Helpers;
using StackExchange.Redis;
using EasyCaching.Core.Configurations;
using Work.Infrastructure.DataSeed;
using Work.Infrastructure.ServicesInterface;

namespace Work.Api
{
    public class Startup
    {
  

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<WorkContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("WorkContext"),
                    b => b.MigrationsAssembly("Work.Infrastructure")
                )
            );
            
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeWork", Version = "v1" });
            });



            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddCors();
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
         

            services.AddEasyCaching(option =>
            {
                option.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new ServerEndPoint("localhost", 6379));

                    config.DBConfig.AllowAdmin = true;
                }, "redis_naim");
            });

            DependencyInjection.Add(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WorkContext cntContext)
        {
            DatabaseInitializer.Initialize(cntContext);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LemoonTicket");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }



       
            app.UseCors(x =>
                x.AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader()


            );
            app.UseAuthentication();

            app.UseRouting();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapHub<PageHub>("/pagehub");
            //});
            
            app.UseMvc();

        }
    }
}
