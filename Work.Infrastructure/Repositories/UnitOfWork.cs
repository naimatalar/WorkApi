using System;
using System.Collections.Generic;
using System.Text;
using Work.Core;
using Work.Infrastructure.RepositoryInterfaces;

namespace Work.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkContext _dbContext;

        public UnitOfWork(WorkContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;

        }


        public IRepositoryBase<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            return new RepositoryBase<TEntity>(_dbContext);
        }

        public int SaveChanges()
        {
            try
            {
    
                return _dbContext.SaveChanges();
            }
            catch
            {
            
                throw;
            }
        }



        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
