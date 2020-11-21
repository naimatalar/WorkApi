using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Work.Infrastructure.Repositories;
using Work.Infrastructure.RepositoryInterfaces;
using Work.Infrastructure.Services;
using Work.Infrastructure.ServicesInterface;

namespace Work.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void Add(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IGeoRepository, GeoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICachingService, CachingService>();


        }
    }
}
