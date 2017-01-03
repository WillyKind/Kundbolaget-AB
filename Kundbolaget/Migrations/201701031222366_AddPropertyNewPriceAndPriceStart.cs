namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyNewPriceAndPriceStart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductInfoes", "NewPrice", c => c.Int());
            AddColumn("dbo.ProductInfoes", "PriceStart", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductInfoes", "PriceStart");
            DropColumn("dbo.ProductInfoes", "NewPrice");
        }
    }
}
