using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Contracts;

namespace ServiceBus.Tests
{
    [TestClass]
    public class ServiceBusRequestResponseTests
    {

        [TestMethod]
        public void InMemoryTest()
        {
            using (var service = Service.InMemory)
            {
                var msg = new RequestItemMessage("Thing");
                var client = service.CreateRequestClient<RequestItem, ItemResponse>();
                //var res = service.Bus.Publish(msg);
                var response = client.Request(msg).Result;

                Assert.AreEqual("something", response.ItemInfo);
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
