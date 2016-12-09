namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsIsRemovedToContactPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactPersons", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactPersons", "IsRemoved");
        }
    }
}
