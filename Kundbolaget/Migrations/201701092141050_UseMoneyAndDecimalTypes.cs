namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UseMoneyAndDecimalTypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductInfoes", "Abv", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ProductInfoes", "PurchasePrice", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.ProductInfoes", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.ProductInfoes", "NewPrice", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.ProductInfoes", "PalletDiscount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetails", "UnitPrice", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.OrderDetails", "TotalPrice", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Orders", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Invoices", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.InvoiceDetails", "FinalPrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceDetails", "FinalPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderDetails", "TotalPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderDetails", "UnitPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.ProductInfoes", "PalletDiscount", c => c.Double());
            AlterColumn("dbo.ProductInfoes", "NewPrice", c => c.Double());
            AlterColumn("dbo.ProductInfoes", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.ProductInfoes", "PurchasePrice", c => c.Double(nullable: false));
            AlterColumn("dbo.ProductInfoes", "Abv", c => c.Double(nullable: false));
        }
    }
}
