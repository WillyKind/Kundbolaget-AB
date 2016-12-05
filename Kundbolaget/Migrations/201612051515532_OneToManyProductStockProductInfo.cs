namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyProductStockProductInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductStocks", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductStocks", "Amount");
        }
    }
}
