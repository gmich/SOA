using System;
using System.Data.Entity;
using System.Linq;
using DAL.Repositories.API;

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

        public void Add(TEntity entityToDelete) =>
            entities.Add(entityToDelete);

        public void Delete(TEntity entityToDelete) =>
            markEntityAs(entityToDelete, EntityState.Deleted);

        public void Update(TEntity entityToDelete) =>
            markEntityAs(entityToDelete, EntityState.Modified);

        public IQueryable<TProjected> Project<TProjected>(Func<TEntity, TProjected> projection) =>
            entities.Select(e => projection(e));

        public IQueryable<TEntity> GetAll() =>
            entities.AsQueryable();

        public IQueryable<TEntity> Query(Predicate<TEntity> query) =>
            entities.Where(e => query(e));

    }

}
