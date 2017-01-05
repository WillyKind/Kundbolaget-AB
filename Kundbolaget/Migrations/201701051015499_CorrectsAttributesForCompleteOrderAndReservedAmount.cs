namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectsAttributesForCompleteOrderAndReservedAmount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "ReservedAmount", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "ReservedAmount", c => c.Int(nullable: false));
        }
    }
}
