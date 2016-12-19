namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsNullableDateTimeStampsToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderPicked", c => c.DateTime());
            AddColumn("dbo.Orders", "OrderTransported", c => c.DateTime());
            AddColumn("dbo.Orders", "OrderDelivered", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderDelivered");
            DropColumn("dbo.Orders", "OrderTransported");
            DropColumn("dbo.Orders", "OrderPicked");
        }
    }
}
