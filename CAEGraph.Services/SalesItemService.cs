using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using CAEGraph.Data;
using CAEGraph.Services.DTOs;
using System.Data.Entity;

namespace CAEGraph.Services
{

    public interface ISalesItemService
    {
        IEnumerable<SaleResult> GetByDate(Constants.Period period, DateTime start, DateTime end);
    }

    public class SalesItemService : ISalesItemService
    {
        private readonly ICAEGraphContext Context;

        public SalesItemService(ICAEGraphContext context)
        {
            Context = context;
        }

        public IEnumerable<SaleResult> GetByDate(Constants.Period period, DateTime start, DateTime end)
        {
            switch (period)
            {
                case Constants.Period.Day:
                    return GetByDateDay(start, end);
                case Constants.Period.Week:
                    return GetByDateWeek(start, end);
                case Constants.Period.Month:
                    return GetByDateMonth(start, end);
                case Constants.Period.Quarter:
                    return GetByDateQuarter(start, end);
                case Constants.Period.Year:
                    return GetByDateYear(start, end);
                default:
                    return null;
            }
        }

        private IEnumerable<SaleResult> GetByDateDay(DateTime start, DateTime end)
        {
            var query = Context.SaleItems.Where(a => a.Date >= start && a.Date <= end)
                .GroupBy(g => new {g.Date.Year, g.Date.Month, g.Date.Day})
                .Select(r => new SaleResult
                {
                    Year = r.Key.Year,
                    Month = r.Key.Month,
                    Day = r.Key.Day,
                    TotalAmount = r.Sum(si => si.Amount),
                    TotalSales = r.Count()
                });
            return query.ToList();
        }

        private IEnumerable<SaleResult> GetByDateWeek(DateTime start, DateTime end)
        {
            var sales = GetByDateDay(start, end);
            var result = sales
                .GroupBy(g => FirstDateOfWeek(new DateTime(g.Year, g.Month, g.Day)))
                .Select(r => new SaleResult
                {
                    Year = r.Key.Year,
                    Month = r.Key.Month,
                    Day = r.Key.Day,
                    TotalAmount = r.Sum(ta => ta.TotalAmount),
                    TotalSales = r.Sum(ts=> ts.TotalSales)
                });
            return result;
        }

        private IEnumerable<SaleResult> GetByDateMonth(DateTime start, DateTime end)
        {
            var sales = GetByDateDay(start, end);
            var result = sales.GroupBy(g => new DateTime(g.Year, g.Month, 1))
                .Select(r => new SaleResult
                {
                    Year = r.Key.Year,
                    Month = r.Key.Month,
                    Day = r.Key.Day,
                    TotalAmount = r.Sum(ta => ta.TotalAmount),
                    TotalSales = r.Sum(ts => ts.TotalSales)
                });
            return result;
        }

        private IEnumerable<SaleResult> GetByDateQuarter(DateTime start, DateTime end)
        {
            var monthQuarter = new Dictionary<int, int>()
            {
                {1, 1}, {2, 4}, {3, 7}, {4, 10}
            };
            var sales = GetByDateDay(start, end);
            var result = sales
                .GroupBy(g => new { g.Year, Quarter = (g.Month - 1) / 3 + 1 })
                .Select(r => new SaleResult
                {
                    Year = r.Key.Year,
                    Month = monthQuarter[r.Key.Quarter],
                    Day = 1,
                    TotalAmount = r.Sum(ta => ta.TotalAmount),
                    TotalSales = r.Sum(ts => ts.TotalSales)
                });
            return result;
        }

        private IEnumerable<SaleResult> GetByDateYear(DateTime start, DateTime end)
        {
            var sales = GetByDateDay(start, end);
            var result = sales
                .GroupBy(g => g.Year)
                .Select(r => new SaleResult
                {
                    Year = r.Key,
                    Month = 1,
                    Day = 1,
                    TotalAmount = r.Sum(ta => ta.TotalAmount),
                    TotalSales = r.Sum(ts => ts.TotalSales)
                });
            return result;
        }

        private DateTime FirstDateOfWeek(DateTime date)
        {
            int delta = DayOfWeek.Sunday - date.DayOfWeek;
            return date.AddDays(delta);
        }
    }
}