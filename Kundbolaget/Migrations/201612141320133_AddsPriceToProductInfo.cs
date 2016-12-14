namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsPriceToProductInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductInfoes", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductInfoes", "Price");
        }
    }
}
