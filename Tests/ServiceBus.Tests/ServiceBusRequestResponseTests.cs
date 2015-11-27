using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Contracts;
using Autofac;
using Moq;
using MassTransit;
using Processing.Consumer;
using Billing.Consumer;
using UserManagement.Consumer;
using DAL;
using DAL.Repositories.API;
using Model;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using ServiceBus.Config;

namespace ServiceBus.Tests
{
    [TestClass]
    public class ServiceBusRequestResponseTests
    {

        [TestMethod]
        [TestCategory("RabbitMQ")]
        public void RabbitMQ_Request_Retrieve_Test()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<DalEFModule>();
            builder.RegisterModule<ProcessingConsumerModule>();
            builder.RegisterModule<BillingConsumerModule>();
            builder.RegisterModule<UserManagementConsumerModule>();
            using (var service = Service.ForRabbitMQ(builder).Start())
            {
                var msg = new RequestItemMessage("Thing");
                var client = service.CreateRequestClient<RequestItem, ItemResponse>();
                //var res = service.Bus.Publish(msg);
                var response = client.Request(msg).Result;
                Assert.AreEqual("something", response.ItemInfo);
            }
        }

        [TestMethod]
        [TestCategory("WinService")]
        public void WinService_Request_Retrieve_Test()
        {
            var bus = BusConfig.ForRabbitMq(Array.Empty<EndpointConfiguration>());
            var handle = bus.Start();
            var msg = new RequestItemMessage("Thing");
            var client = bus.CreateRequestClient<RequestItem, ItemResponse>(
                new Uri("rabbitmq://localhost/service_queue"),
                TimeSpan.FromSeconds(10));

            var response = client.Request(msg).Result;
            handle.Dispose();

            Assert.AreEqual("something", response.ItemInfo);
        }

        [TestMethod]
        [TestCategory("Mock")]
        public void InMemory_Request_Retrieve_Mock_Test()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ProcessingConsumerModule>();
            builder.RegisterModule<BillingConsumerModule>();
            builder.RegisterModule<UserManagementConsumerModule>();

            var itemList = new List<Item>();
            itemList.Add(new Item { Info = "mockResponse" });
            var mockItemRepository = new Mock<IRepository<Item>>();

            mockItemRepository.Setup(rep =>
                rep.Query(It.IsAny<Expression<Func<Item, bool>>>()))
                   .Returns(itemList.AsQueryable());
            builder.Register(c =>
                mockItemRepository.Object).As<IRepository<Item>>();

            using (var service = Service.InMemory(builder).Start())
            {
                var msg = new RequestItemMessage("mockSelection");
                var client = service.CreateRequestClient<RequestItem, ItemResponse>();
                var response = client.Request(msg).Result;
                Assert.AreEqual("mockResponse", response.ItemInfo);
            }
        }

        public class RequestItemMessage : RequestItem
        {
            public RequestItemMessage(string itemName)
            {
                ItemName = itemName;
            }

            public string ItemName { get; }
        }
    }
}
