using CAEGraph.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CAEGraph.Services;
using CAEGraph.Models;

namespace CAEGraph.Controllers
{
    public class SaleItemsController : ApiController
    {
        private readonly ISalesItemService SalesItemService;

        public SaleItemsController(ISalesItemService salesItemsService)
        {
            SalesItemService = salesItemsService;
        }

        // GET api/SaleItems
        public IEnumerable<SaleResultViewModel> Get(Constants.Period period, DateTime start, DateTime end)
        {
            var result = SalesItemService.GetByDate(period, start, end)
                .Select(a => new SaleResultViewModel(
                    new DateTime(a.Year, a.Month, a.Day),
                    a.TotalAmount,
                    a.TotalSales)
                );

            return result;
        }
    }
}
