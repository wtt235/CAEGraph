using System.Data.Common;
using System.Data.Entity;
using CAEGraph.Data.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CAEGraph.Data
{

    public interface ICAEGraphContext
    {
        DbSet<SaleItem> SaleItems { get; set; }
    }

    public class CAEGraphContext : DbContext, ICAEGraphContext
    {
        public CAEGraphContext() : base("CAEGraphContext")
        {
        }

        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
