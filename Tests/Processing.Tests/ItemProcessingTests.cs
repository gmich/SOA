using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using DAL;
using Processing.Core;
using Processing.Core.API;

namespace Processing.Tests
{
    [TestClass]
    public class ItemProcessingTests
    {
        private readonly IContainer container;

        public ItemProcessingTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DalEFModule>();
            builder.RegisterModule<ProcessingCoreModule>();
            container = builder.Build();

        }

        public TService GetService<TService>()
            where TService : class
        {
            return container.Resolve<TService>();
        }

        //[TestMethod]
        public void ItemRetrievalTest()
        {
            var processingService = GetService<IProcessingService>();

            var items = processingService.GetItemsByName("Thing");

            Assert.AreEqual(1, items.Count());
        }
    }
}
