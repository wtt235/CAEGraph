using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace CAEGraph.Models
{
    public class SaleResultViewModel
    {
        public SaleResultViewModel(DateTime date, decimal totalAmount, int totalSales)
        {
            Date = date.ToString("yyyy-MM-dd");
            //var cal = CultureInfo.CurrentCulture.Calendar;
            //WeekNumber = cal.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            //int[] quarters = new int[] { 4, 4, 4, 1, 1, 1, 2, 2, 2, 3, 3, 3 };
            //Quarter = quarters[date.Month - 1];
            TotalAmount = totalAmount;
            TotalSales = totalSales;
        }

        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalSales { get; set; }
    }
}