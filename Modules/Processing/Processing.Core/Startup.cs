using Autofac;
using DAL;
using DAL.Repositories.API;
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
            builder.RegisterModule<DalEFModule>();

            var container = builder.Build();
        }


    }
}
