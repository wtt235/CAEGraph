using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAEGraph.Services.DTOs
{
    public class SaleResult
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalSales { get; set; }
    }
}