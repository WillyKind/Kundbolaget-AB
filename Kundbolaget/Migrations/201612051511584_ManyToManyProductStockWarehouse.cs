namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyProductStockWarehouse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductStockWarehouses",
                c => new
                    {
                        ProductStock_Id = c.Int(nullable: false),
                        Warehouse_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductStock_Id, t.Warehouse_Id })
                .ForeignKey("dbo.ProductStocks", t => t.ProductStock_Id, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id, cascadeDelete: true)
                .Index(t => t.ProductStock_Id)
                .Index(t => t.Warehouse_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductStockWarehouses", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.ProductStockWarehouses", "ProductStock_Id", "dbo.ProductStocks");
            DropIndex("dbo.ProductStockWarehouses", new[] { "Warehouse_Id" });
            DropIndex("dbo.ProductStockWarehouses", new[] { "ProductStock_Id" });
            DropTable("dbo.ProductStockWarehouses");
        }
    }
}
