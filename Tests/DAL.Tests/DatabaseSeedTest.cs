using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Persistence;
using System.Linq;
using Model;

namespace DAL.Tests
{
    [TestClass]
    public class DatabaseSeedTest
    {
        //[TestMethod]
        public void DatabaseSeed()
        {
            var testItem = new Item
            {
                ItemId = 1,
                Name = "Thing",
                Info = "Something"
            };
            using (var context = new ApplicationDbContext())
            {
                if (!context.Items.Any(item => item.ItemId == 1))
                {
                    context.Items.Add(testItem);
                }
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext())
            {
                Assert.AreEqual(context.Items.Where(item => item.ItemId == 1).First().Name, "Thing");
            }
        }
    }
}
