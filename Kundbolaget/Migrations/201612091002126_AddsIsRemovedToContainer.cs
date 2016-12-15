namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToContainer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Containers", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Containers", "IsRemoved");
        }
    }
}
