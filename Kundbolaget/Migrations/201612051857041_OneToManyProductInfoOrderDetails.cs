namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyProductInfoOrderDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ProductInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductInfoes", t => t.ProductInfoId, cascadeDelete: true)
                .Index(t => t.ProductInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductInfoId", "dbo.ProductInfoes");
            DropIndex("dbo.OrderDetails", new[] { "ProductInfoId" });
            DropTable("dbo.OrderDetails");
        }
    }
}
