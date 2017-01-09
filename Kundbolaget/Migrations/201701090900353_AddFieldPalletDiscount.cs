namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldPalletDiscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductInfoes", "PalletDiscount", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductInfoes", "PalletDiscount");
        }
    }
}
