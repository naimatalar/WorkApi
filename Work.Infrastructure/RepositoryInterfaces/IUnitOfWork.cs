using System;
using Work.Core;

namespace Work.Infrastructure.RepositoryInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBase<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }

}
