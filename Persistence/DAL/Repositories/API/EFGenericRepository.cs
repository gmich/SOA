using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class EFGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> entities;

        public EFGenericRepository(DbSet<TEntity> entities)
        {
            this.entities = entities;
        }

        public IQueryable<TProjected> Project<TProjected>(Func<TEntity, TProjected> projection) =>
            entities.Select(e => projection(e));

        public IQueryable<TEntity> GetAll() =>
            entities.AsQueryable();
        
        public IQueryable<TEntity> Query(Predicate<TEntity> query) => 
            entities.Where(e => query(e));

    }

}
