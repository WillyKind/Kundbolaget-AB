namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyProductProductGroup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductGroups", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Products", "ProductGroupId");
            AddForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            AlterColumn("dbo.ProductGroups", "Name", c => c.String());
        }
    }
}
