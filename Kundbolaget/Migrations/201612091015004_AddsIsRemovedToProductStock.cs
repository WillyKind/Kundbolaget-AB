namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToProductStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductStocks", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductStocks", "IsRemoved");
        }
    }
}
