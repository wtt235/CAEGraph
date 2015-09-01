using System;
using System.ComponentModel.DataAnnotations;

namespace CAEGraph.Data.Entities
{
    public class SaleItem
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
