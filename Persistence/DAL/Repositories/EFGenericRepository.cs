using System;
using System.Data.Entity;
using System.Linq;
using DAL.Repositories.API;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    internal class EFGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> entities;
        private readonly Action<TEntity, EntityState> markEntityAs;

        public EFGenericRepository(Action<TEntity, EntityState> markEntityAs, DbSet<TEntity> entities)
        {
            this.entities = entities;
            this.markEntityAs = markEntityAs;
        }

        public void Replace<TKey>(Func<TEntity, TKey> keyRetriever, TEntity entity)
        {
            if (entities
                .Local
                .Any(localEntity =>
                    keyRetriever(localEntity).
                    Equals(keyRetriever(entity))))
            {
                markEntityAs(entities.Local
                    .Where(localEntity =>
                      keyRetriever(localEntity).Equals(keyRetriever(entity)))
                      .First(), EntityState.Detached);
            }
            markEntityAs(entity, EntityState.Modified);
        }

        public void Add(TEntity entityToDelete) =>
            entities.Add(entityToDelete);

        public void Delete(TEntity entityToDelete) =>
            markEntityAs(entityToDelete, EntityState.Deleted);

        public void Update(TEntity entityToDelete) =>
            markEntityAs(entityToDelete, EntityState.Modified);

        public IQueryable<TProjected> Project<TProjected>(Expression<Func<TEntity, TProjected>> projection) =>
            entities.Select(projection);

        public IQueryable<TEntity> Entries() =>
            entities.AsQueryable();

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> query) =>
            entities.Where(query);

    }

}
