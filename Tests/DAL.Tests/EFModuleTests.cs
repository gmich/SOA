using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Persistence;
using System.Linq;
using Model;
using Autofac;
using DAL.Repositories.API;

namespace DAL.Tests
{
    [TestClass]
    public class EFModuleTests
    {
        private readonly IContainer container;
        private readonly IUnitOfWork unitOfWork;

        public EFModuleTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DalEFModule>();
            container = builder.Build();

            unitOfWork = container.Resolve<IUnitOfWork>();
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            return container.Resolve<IRepository<TEntity>>();
        }
        
        //[TestMethod]
        public void ItemRepositoryTest()
        {
            var itemRepo = GetRepository<Item>();

            itemRepo.Add(new Item
            {
                ItemId = 1,
                Name = "name",
                Info = "info"
            });

            unitOfWork.TryInScope(c => c.Commit(),
                                  onException: (rollback, ex) => rollback());

        }

    }
}
