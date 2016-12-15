namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeManyToManyWareHouseProductStockToOneToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductStockWarehouses", "ProductStock_Id", "dbo.ProductStocks");
            DropForeignKey("dbo.ProductStockWarehouses", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.Companies", "DeliveryAddressId", "dbo.Addresses");
            DropIndex("dbo.ProductStockWarehouses", new[] { "ProductStock_Id" });
            DropIndex("dbo.ProductStockWarehouses", new[] { "Warehouse_Id" });
            AddColumn("dbo.ProductStocks", "WarehouseId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductStocks", "WarehouseId");
            AddForeignKey("dbo.ProductStocks", "WarehouseId", "dbo.Warehouses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Companies", "DeliveryAddressId", "dbo.Addresses", "Id", cascadeDelete: true);
            DropTable("dbo.ProductStockWarehouses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductStockWarehouses",
                c => new
                    {
                        ProductStock_Id = c.Int(nullable: false),
                        Warehouse_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductStock_Id, t.Warehouse_Id });
            
            DropForeignKey("dbo.Companies", "DeliveryAddressId", "dbo.Addresses");
            DropForeignKey("dbo.ProductStocks", "WarehouseId", "dbo.Warehouses");
            DropIndex("dbo.ProductStocks", new[] { "WarehouseId" });
            DropColumn("dbo.ProductStocks", "WarehouseId");
            CreateIndex("dbo.ProductStockWarehouses", "Warehouse_Id");
            CreateIndex("dbo.ProductStockWarehouses", "ProductStock_Id");
            AddForeignKey("dbo.Companies", "DeliveryAddressId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.ProductStockWarehouses", "Warehouse_Id", "dbo.Warehouses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductStockWarehouses", "ProductStock_Id", "dbo.ProductStocks", "Id", cascadeDelete: true);
        }
    }
}
