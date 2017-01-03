namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentPropertyOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Comment");
        }
    }
}
