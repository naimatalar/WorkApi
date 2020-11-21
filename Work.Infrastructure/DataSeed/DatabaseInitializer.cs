using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Work.Core;
using Work.Infrastructure.Services;
using Work.Infrastructure.ServicesInterface;


namespace Work.Infrastructure.DataSeed
{
    public static class DatabaseInitializer
    {

        public static void Initialize(WorkContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            if (!dbContext.Users.Any())
            {
                var json = new WebClient().DownloadString("https://jsonplaceholder.typicode.com/users");
                var list = JsonConvert.DeserializeObject<List<JsonUserList>>(json);
                CryptoService cs = new CryptoService();
                foreach (var item in list)
                {
                    var salt = new Random().Next(10000000, 99999999).ToString();

                    var addresslist = new List<Core.Address>();
                    var companylist=new List<Core.Company>();
                    addresslist.Add(new Core.Address
                    {
                        City = item.address.city,
                        Street = item.address.street,
                        Suite = item.address.suite,
                        ZipCode = item.address.zipcode,
                        
                    });
                    companylist.Add(new Core.Company
                    {
                        Name = item.company.name,
                        Bs = item.company.bs,
                        CatchPhrase = item.company.catchPhrase,
                        
                    });
                   
                    

                    var user=new User
                   {
                       Name = item.name,
                       Email = item.email,
                       PasswordHash  = cs.Encrypt("test123", salt),
                       PasswordSalt = salt,
                       Phone = item.phone,
                       UserName = item.username,
                       Website = item.website,
                       Addresses= addresslist,
                       Companies = companylist
                    };

                    dbContext.Users.Add(user);
                }


                dbContext.SaveChanges();
            }

          

        }

    }
}
