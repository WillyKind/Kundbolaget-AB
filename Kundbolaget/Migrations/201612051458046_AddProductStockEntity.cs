namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductStockEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductInfoes", t => t.ProductInfoId, cascadeDelete: true)
                .Index(t => t.ProductInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductStocks", "ProductInfoId", "dbo.ProductInfoes");
            DropIndex("dbo.ProductStocks", new[] { "ProductInfoId" });
            DropTable("dbo.ProductStocks");
        }
    }
}
