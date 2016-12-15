namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "IsRemoved");
        }
    }
}
