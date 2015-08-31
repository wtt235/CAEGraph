namespace CAEGraph.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Table_SaleItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaleItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SaleItem");
        }
    }
}
