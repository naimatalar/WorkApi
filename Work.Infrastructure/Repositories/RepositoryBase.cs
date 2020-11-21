using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Work.Core;
using Work.Infrastructure.RepositoryInterfaces;

namespace Work.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected WorkContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(WorkContext context)
        {
            if (context == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = context;
            _dbSet = context.Set<TEntity>();

        }
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;

        }

        public TEntity GetById(Guid Id)
        {
            return _dbSet.Where(x => x.Id == Id).FirstOrDefault();

        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);

        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }




    }
}
