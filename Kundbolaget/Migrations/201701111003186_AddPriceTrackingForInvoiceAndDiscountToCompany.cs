namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceTrackingForInvoiceAndDiscountToCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Discount", c => c.Double(nullable: false));
            AddColumn("dbo.Invoices", "PriceWithCompanyDiscount", c => c.Double(nullable: false));
            AddColumn("dbo.Invoices", "OriginalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Invoices", "PriceWithPalletDiscount", c => c.Double(nullable: false));
            DropColumn("dbo.Invoices", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Invoices", "PriceWithPalletDiscount");
            DropColumn("dbo.Invoices", "OriginalPrice");
            DropColumn("dbo.Invoices", "PriceWithCompanyDiscount");
            DropColumn("dbo.Companies", "Discount");
        }
    }
}
