using System;
using System.Collections.Generic;
using System.Text;
using Work.Core;
using Work.Infrastructure.RepositoryInterfaces;

namespace Work.Infrastructure.Repositories
{
    public class GeoRepository : RepositoryBase<Geo>, IGeoRepository
    {
        public GeoRepository(WorkContext context) : base(context)
        {
        }
    }
}
