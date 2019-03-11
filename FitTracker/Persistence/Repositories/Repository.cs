using FitTracker.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public async virtual Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate) 
        {
            if (predicate == null)
                return new List<TEntity>();

            return await Context.Set<TEntity>().Where(predicate)
                .ToListAsync();
        }

        public virtual TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }


        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>()
                .ToList();
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
