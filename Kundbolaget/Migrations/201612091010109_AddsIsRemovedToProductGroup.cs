namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToProductGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductGroups", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductGroups", "IsRemoved");
        }
    }
}
