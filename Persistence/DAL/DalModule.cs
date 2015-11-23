using Autofac;
using DAL.Persistence;
using DAL.Repositories;
using System;
using System.Data.Entity;

namespace DAL
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ApplicationDbContext())
                   .AsSelf()
                   .InstancePerLifetimeScope();
            
            builder.RegisterRepository(context => context.Items);
        }
    }


    public static class ContainerBuilderExtensions
    {
        public static void RegisterRepository<TEntity>(this ContainerBuilder builder, 
                                            Func<ApplicationDbContext, DbSet<TEntity>> dbSetGettter)
            where TEntity : class
        {
            builder
                .Register(c => new EFGenericRepository<TEntity>(dbSetGettter(c.Resolve<ApplicationDbContext>())))
                           .As<IRepository<TEntity>>();
        }
    }
}
