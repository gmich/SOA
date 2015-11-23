using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TProjected> Project<TProjected>(Func<TEntity,TProjected> projection);
        
        IQueryable<TEntity> Query(Predicate<TEntity> query);
    }
}
