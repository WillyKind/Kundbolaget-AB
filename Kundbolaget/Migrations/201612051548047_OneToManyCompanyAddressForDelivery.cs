namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyCompanyAddressForDelivery : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Companies", "AddressId", "dbo.Addresses");
            CreateIndex("dbo.Companies", "DeliveryAddressId");
            AddForeignKey("dbo.Companies", "DeliveryAddressId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Companies", "AddressId", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Companies", "DeliveryAddressId", "dbo.Addresses");
            DropIndex("dbo.Companies", new[] { "DeliveryAddressId" });
            AddForeignKey("dbo.Companies", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
