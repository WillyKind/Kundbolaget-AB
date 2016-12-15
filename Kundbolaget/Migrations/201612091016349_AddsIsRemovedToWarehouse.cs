namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToWarehouse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Warehouses", "IsRemoved");
        }
    }
}
