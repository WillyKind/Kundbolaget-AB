namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiscountToCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Discount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Discount");
        }
    }
}
