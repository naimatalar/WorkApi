using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Work.Core
{
    public class User :BaseEntity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Company> Companies { get; set; }

    }
}
