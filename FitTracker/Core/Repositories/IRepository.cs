using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Core.Repositories
{
    public interface IRepository<TEntity> 
    {
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
