namespace CAEGraph.Data.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CAEGraph.Data.CAEGraphContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CAEGraph.Data.CAEGraphContext context)
        {
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.SaleItem");
            TimeSpan timeSpan = new DateTime(2064, 1, 1) - new DateTime(2014, 1, 1);
            var randomTest = new Random();
            for (int i = 0; i < 18000; i++)
            {
                TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
                DateTime newDate = new DateTime(2014, 1, 1) + newSpan;
                context.SaleItems.Add(new SaleItem() { Date = newDate, Amount = randomTest.Next(0, 501) });
            }
            context.SaveChanges();
        }
    }
}
