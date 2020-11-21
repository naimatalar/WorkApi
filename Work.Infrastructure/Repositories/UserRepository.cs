#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Work.Core;
using Work.Infrastructure.RepositoryInterfaces;
using Work.Infrastructure.ServicesInterface;

namespace Work.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(WorkContext context) : base(context)
        {
        }

        public User? LoginControl(string key,string password , ICryptoService c)
        {
           var user= this.Where(x => x.Email == key || x.UserName==key).FirstOrDefault();
           if (user==null)
           {
               return null;
           }
           var passwordView = c.Decrypt(user.PasswordHash, user.PasswordSalt);
           if (passwordView==password)
           {
               return user;
           }
            return null;
        }
    }
}
