using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAEGraph.Services.DTOs
{
    public class SaleResult
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalSales { get; set; }
    }
}