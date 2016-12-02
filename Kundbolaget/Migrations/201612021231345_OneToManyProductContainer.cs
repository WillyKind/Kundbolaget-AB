namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyProductContainer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Containers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Products", "ContainerId");
            AddForeignKey("dbo.Products", "ContainerId", "dbo.Containers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ContainerId", "dbo.Containers");
            DropIndex("dbo.Products", new[] { "ContainerId" });
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Containers", "Name", c => c.String());
        }
    }
}
