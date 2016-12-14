namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsPricePropsToOrderDEtails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "UnitPrice", c => c.Double(nullable: false));
            AddColumn("dbo.OrderDetails", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "TotalPrice");
            DropColumn("dbo.OrderDetails", "UnitPrice");
        }
    }
}
