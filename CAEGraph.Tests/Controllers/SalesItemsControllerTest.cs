using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAEGraph;
using CAEGraph.Controllers;
using CAEGraph.Data;
using CAEGraph.Data.Entities;
using CAEGraph.Services;
using CAEGraph.Services.DTOs;
using Moq;

namespace CAEGraph.Tests.Controllers
{
    /*
    * Not really much to test here, since the service does all the work.
    */
    [TestClass]
    public class SalesItemsControllerTest
    {

        private List<SaleResult> data = new List<SaleResult>
            {
                new SaleResult {Day = 1, Month = 1, Year = 2015,TotalAmount = 1, TotalSales = 1},
                new SaleResult {Day = 1, Month = 1, Year = 2016,TotalAmount = 1, TotalSales = 1},
                new SaleResult {Day = 1, Month = 1, Year = 2017,TotalAmount = 1, TotalSales = 1},
                new SaleResult {Day = 1, Month = 1, Year = 2018,TotalAmount = 1, TotalSales = 1},
            };

        private Mock<ISalesItemService> service;

        [TestInitialize]
        public void TestSetup()
        {
            service = new Mock<ISalesItemService>();
            service.Setup(c => c.GetByDate(
                It.IsAny<Constants.Period>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>())).Returns(data);
        }

        [TestMethod]
        public void Get()
        {
            var controller = new SaleItemsController(service.Object);
            var result =
                controller.Get(Constants.Period.Day, DateTime.MinValue, DateTime.MinValue);
            Assert.AreEqual(result.Count(), 4);

        }
    }
}
