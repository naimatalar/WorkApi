using System;
using System.Collections.Generic;
using System.Text;
using Work.Core;
using Work.Infrastructure.RepositoryInterfaces;

namespace Work.Infrastructure.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>,ICompanyRepository
    {
        public CompanyRepository(WorkContext context) : base(context)
        {
        }
    }
}
