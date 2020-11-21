using System;
using System.Collections.Generic;
using System.Text;
using Work.Core;
using Work.Infrastructure.RepositoryInterfaces;

namespace Work.Infrastructure.Repositories
{
    public class AddressRepository:RepositoryBase<Address>,IAddressRepository
    {
        public AddressRepository(WorkContext context) : base(context)
        {
        }
    }
}
