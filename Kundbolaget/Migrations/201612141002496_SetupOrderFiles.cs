namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetupOrderFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Containers", "Volume", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "CustomerOrderId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "WishedDeliveryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "WishedDeliveryDate");
            DropColumn("dbo.Orders", "CustomerOrderId");
            DropColumn("dbo.Containers", "Volume");
        }
    }
}
