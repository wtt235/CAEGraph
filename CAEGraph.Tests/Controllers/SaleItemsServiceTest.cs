using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CAEGraph.Data.Entities;
using CAEGraph.Data;
using CAEGraph.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CAEGraph.Tests.Controllers
{
    [TestClass]
    public class SaleItemsServiceTest
    {
        private List<SaleItem> data = new List<SaleItem>
            {
                new SaleItem {Id = 1, Date = new DateTime(2015, 8, 3), Amount = 1},
                new SaleItem {Id = 2, Date = new DateTime(2015, 8, 5), Amount = 1},
                new SaleItem {Id = 3, Date = new DateTime(2015, 9, 1), Amount = 1},
                new SaleItem {Id = 4, Date = new DateTime(2015, 10, 1), Amount = 1},
                new SaleItem {Id = 5, Date = new DateTime(2016, 1, 1), Amount = 1},
                new SaleItem {Id = 6, Date = new DateTime(2018, 1, 1), Amount = 1},
            };

        private Mock<ICAEGraphContext> context;

        [TestInitialize]
        public void TestSetup()
        {
            var set = new Mock<DbSet<SaleItem>>().SetupData(data);
            context = new Mock<ICAEGraphContext>();
            context.Setup(c => c.SaleItems).Returns(set.Object);
        }

        [TestMethod]
        public void GetByDay()
        {
            var service = new SalesItemService(context.Object);
            var start = new DateTime(2015, 1, 1);
            var end = new DateTime(2018, 1, 1);
            var result = service.GetByDate(Constants.Period.Day, start, end);
            Assert.AreEqual(result.Count(), 6);
        }

        [TestMethod]
        public void GetByWeek()
        {
            var service = new SalesItemService(context.Object);
            var start = new DateTime(2015, 8, 1);
            var end = new DateTime(2018, 1, 1);
            var result = service.GetByDate(Constants.Period.Week, start, end);
            Assert.AreEqual(result.Count(), 5);
        }

        [TestMethod]
        public void GetByMonth()
        {
            var service = new SalesItemService(context.Object);
            var start = new DateTime(2015, 8, 1);
            var end = new DateTime(2018, 1, 1);
            var result = service.GetByDate(Constants.Period.Month, start, end);
            Assert.AreEqual(result.Count(), 5);
        }

        [TestMethod]
        public void GetByYear()
        {
            var service = new SalesItemService(context.Object);
            var start = new DateTime(2015, 8, 1);
            var end = new DateTime(2018, 1, 1);
            var result = service.GetByDate(Constants.Period.Year, start, end);
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        public void AggregatesValues()
        {
            var service = new SalesItemService(context.Object);
            var start = new DateTime(2015, 8, 3);
            var end = new DateTime(2015, 8, 5);
            var result = service.GetByDate(Constants.Period.Week, start, end);
            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.First().TotalAmount, 2);
            Assert.AreEqual(result.First().TotalSales, 2);
        }

        [TestMethod]
        public void GetByBadDates()
        {
            var service = new SalesItemService(context.Object);
            var start = new DateTime(2015, 8, 1);
            var end = new DateTime(2018, 1, 1);
            var result = service.GetByDate(Constants.Period.Year, end, start);  //end and start are reversed
            Assert.AreEqual(result.Count(), 0);
        }

    }
}
