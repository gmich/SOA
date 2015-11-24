using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceBus.Tests
{
    [TestClass]
    public class InitializationTests
    {
        //[TestMethod]
        public void ServiceBus_Resolves_Consumers_Correctly()
        {
            using (var bus = new Service())
            { } 
        }
    }
}
