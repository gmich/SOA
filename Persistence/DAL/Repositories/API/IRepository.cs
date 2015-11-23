using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.API
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entityToDelete);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToDelete);

        IQueryable<TEntity> GetAll();

        IQueryable<TProjected> Project<TProjected>(Func<TEntity, TProjected> projection);

        IQueryable<TEntity> Query(Predicate<TEntity> query);

    }
}
