namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "IsRemoved");
        }
    }
}
