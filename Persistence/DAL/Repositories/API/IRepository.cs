using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories.API
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entityToDelete);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToDelete);
                
        void Replace<TKey>(Func<TEntity, TKey> keyRetriever, TEntity entity);

        IQueryable<TEntity> Entries();

        IQueryable<TProjected> Project<TProjected>(Expression<Func<TEntity, TProjected>> projection);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> query);

    }
}
