using Autofac;
using DAL.Persistence;
using DAL.Repositories;
using DAL.Repositories.API;
using System;
using System.Data.Entity;

namespace DAL
{
    public class DalEFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ApplicationDbContext())
                   .AsSelf()
                   .InstancePerLifetimeScope();

            builder.Register(c => new EFUnitOfWork(c.Resolve<ApplicationDbContext>()))
                    .As<IUnitOfWork>()
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
                .Register(c =>
                {
                    var context = c.Resolve<ApplicationDbContext>();
                    return new EFGenericRepository<TEntity>(
                        (entity, state) => context.Entry(entity).State = state,
                        dbSetGettter(context));
                })
                .As<IRepository<TEntity>>();
        }
    }
}
