namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsRemoved");
        }
    }
}
