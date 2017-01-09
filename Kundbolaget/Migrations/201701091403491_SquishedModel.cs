namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SquishedModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        AddressId = c.Int(nullable: false),
                        ContactPersonId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        DeliveryAddressId = c.Int(nullable: false),
                        ParentCompanyId = c.Int(),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.ContactPersons", t => t.ContactPersonId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.DeliveryAddressId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.ParentCompanyId)
                .Index(t => t.AddressId)
                .Index(t => t.ContactPersonId)
                .Index(t => t.CountryId)
                .Index(t => t.DeliveryAddressId)
                .Index(t => t.ParentCompanyId);
            
            CreateTable(
                "dbo.ContactPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AmmountOfStorageSpace = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        ContactPersonId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.ContactPersons", t => t.ContactPersonId, cascadeDelete: true)
                .Index(t => t.ContactPersonId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.ProductStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ProductInfoId = c.Int(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductInfoes", t => t.ProductInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.ProductInfoId)
                .Index(t => t.WarehouseId);
            
            CreateTable(
                "dbo.ProductInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContainerId = c.Int(nullable: false),
                        VolumeId = c.Int(nullable: false),
                        ProductGroupId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Abv = c.Double(nullable: false),
                        TradingMargin = c.Double(nullable: false),
                        PurchasePrice = c.Double(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                        NewPrice = c.Double(),
                        PriceStart = c.DateTime(),
                        PalletDiscount = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Containers", t => t.ContainerId, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Volumes", t => t.VolumeId, cascadeDelete: true)
                .Index(t => t.ContainerId)
                .Index(t => t.VolumeId)
                .Index(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.Containers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Volume = c.Double(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ProductInfoId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.ProductInfoes", t => t.ProductInfoId, cascadeDelete: true)
                .Index(t => t.ProductInfoId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        CustomerOrderId = c.Int(nullable: false),
                        WishedDeliveryDate = c.DateTime(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                        OrderPicked = c.DateTime(),
                        OrderTransported = c.DateTime(),
                        OrderDelivered = c.DateTime(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CustomerOrderId = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        FinalPrice = c.Double(nullable: false),
                        ProductInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.ProductInfoes", t => t.ProductInfoId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.ProductInfoId);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Volumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Milliliter = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CountryCode = c.String(nullable: false),
                        Region = c.String(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "ParentCompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "DeliveryAddressId", "dbo.Addresses");
            DropForeignKey("dbo.Companies", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.ProductStocks", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.ProductStocks", "ProductInfoId", "dbo.ProductInfoes");
            DropForeignKey("dbo.ProductInfoes", "VolumeId", "dbo.Volumes");
            DropForeignKey("dbo.ProductInfoes", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.OrderDetails", "ProductInfoId", "dbo.ProductInfoes");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Invoices", "Id", "dbo.Orders");
            DropForeignKey("dbo.InvoiceDetails", "ProductInfoId", "dbo.ProductInfoes");
            DropForeignKey("dbo.InvoiceDetails", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Orders", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.ProductInfoes", "ContainerId", "dbo.Containers");
            DropForeignKey("dbo.Warehouses", "ContactPersonId", "dbo.ContactPersons");
            DropForeignKey("dbo.Warehouses", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Companies", "ContactPersonId", "dbo.ContactPersons");
            DropForeignKey("dbo.Companies", "AddressId", "dbo.Addresses");
            DropIndex("dbo.ProductGroups", new[] { "CategoryId" });
            DropIndex("dbo.InvoiceDetails", new[] { "ProductInfoId" });
            DropIndex("dbo.InvoiceDetails", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "CompanyId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductInfoId" });
            DropIndex("dbo.ProductInfoes", new[] { "ProductGroupId" });
            DropIndex("dbo.ProductInfoes", new[] { "VolumeId" });
            DropIndex("dbo.ProductInfoes", new[] { "ContainerId" });
            DropIndex("dbo.ProductStocks", new[] { "WarehouseId" });
            DropIndex("dbo.ProductStocks", new[] { "ProductInfoId" });
            DropIndex("dbo.Warehouses", new[] { "AddressId" });
            DropIndex("dbo.Warehouses", new[] { "ContactPersonId" });
            DropIndex("dbo.Companies", new[] { "ParentCompanyId" });
            DropIndex("dbo.Companies", new[] { "DeliveryAddressId" });
            DropIndex("dbo.Companies", new[] { "CountryId" });
            DropIndex("dbo.Companies", new[] { "ContactPersonId" });
            DropIndex("dbo.Companies", new[] { "AddressId" });
            DropTable("dbo.Countries");
            DropTable("dbo.Volumes");
            DropTable("dbo.Categories");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.InvoiceDetails");
            DropTable("dbo.Invoices");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Containers");
            DropTable("dbo.ProductInfoes");
            DropTable("dbo.ProductStocks");
            DropTable("dbo.Warehouses");
            DropTable("dbo.ContactPersons");
            DropTable("dbo.Companies");
            DropTable("dbo.Addresses");
        }
    }
}
