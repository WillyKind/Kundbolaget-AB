namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsOrderCompleteBoolToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderComplete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderComplete");
        }
    }
}
