using System;
using System.Linq;
using System.Linq.Expressions;
using Work.Core;

namespace Work.Infrastructure.RepositoryInterfaces
{
   public interface IRepositoryBase<TEntity>  where TEntity:BaseEntity
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
        TEntity GetById(Guid Id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
    }
}
