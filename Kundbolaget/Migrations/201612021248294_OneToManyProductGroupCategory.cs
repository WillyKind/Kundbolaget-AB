namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyProductGroupCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.ProductGroups", "CategoryId");
            AddForeignKey("dbo.ProductGroups", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductGroups", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ProductGroups", new[] { "CategoryId" });
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
