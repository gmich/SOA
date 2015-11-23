using Autofac;
using DAL;
using DAL.Repositories;
using System;

namespace Processing.Core
{
    public delegate IRepository<TEntity> RepositoryFactory<TEntity>()
        where TEntity : class;

    public class Startup
    {
        public Startup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DalModule>();

            var container = builder.Build();
        }


    }
}
