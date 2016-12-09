namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToProductInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductInfoes", "IsRemoved", c => c.Boolean(nullable: false));
            DropColumn("dbo.ProductInfoes", "Removed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductInfoes", "Removed", c => c.Boolean(nullable: false));
            DropColumn("dbo.ProductInfoes", "IsRemoved");
        }
    }
}
