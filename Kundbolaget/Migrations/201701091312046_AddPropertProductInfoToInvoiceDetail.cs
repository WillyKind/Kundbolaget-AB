namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertProductInfoToInvoiceDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceDetails", "ProductInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoiceDetails", "ProductInfoId");
            AddForeignKey("dbo.InvoiceDetails", "ProductInfoId", "dbo.ProductInfoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceDetails", "ProductInfoId", "dbo.ProductInfoes");
            DropIndex("dbo.InvoiceDetails", new[] { "ProductInfoId" });
            DropColumn("dbo.InvoiceDetails", "ProductInfoId");
        }
    }
}
