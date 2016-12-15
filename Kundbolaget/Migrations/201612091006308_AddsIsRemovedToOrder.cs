namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsRemoved");
        }
    }
}
