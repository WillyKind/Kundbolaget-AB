namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyAdressWareHouse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Warehouses", "AddressId");
            AddForeignKey("dbo.Warehouses", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warehouses", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Warehouses", new[] { "AddressId" });
            DropColumn("dbo.Warehouses", "AddressId");
        }
    }
}
