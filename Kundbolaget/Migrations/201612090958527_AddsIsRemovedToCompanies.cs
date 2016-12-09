namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToCompanies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "IsRemoved");
        }
    }
}
