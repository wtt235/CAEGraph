using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAEGraph.Models
{
    public class SaleItemViewModel
    {
        public int Id { get; set; }
        public string SaleDate { get; set; }
        public decimal SaleAmount { get; set; }
    }
}