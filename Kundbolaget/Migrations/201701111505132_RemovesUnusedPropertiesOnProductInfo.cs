namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovesUnusedPropertiesOnProductInfo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductInfoes", "TradingMargin");
            DropColumn("dbo.ProductInfoes", "PurchasePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductInfoes", "PurchasePrice", c => c.Double(nullable: false));
            AddColumn("dbo.ProductInfoes", "TradingMargin", c => c.Double(nullable: false));
        }
    }
}
