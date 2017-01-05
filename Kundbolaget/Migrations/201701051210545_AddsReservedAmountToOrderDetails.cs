namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsReservedAmountToOrderDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ReservedAmount", c => c.Int());
            AddColumn("dbo.Orders", "OrderComplete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderComplete");
            DropColumn("dbo.OrderDetails", "ReservedAmount");
        }
    }
}
