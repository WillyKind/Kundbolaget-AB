namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnToManyCompanyToCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Company_Id", c => c.Int());
            CreateIndex("dbo.Companies", "Company_Id");
            AddForeignKey("dbo.Companies", "Company_Id", "dbo.Companies", "Id");
            DropColumn("dbo.Companies", "GroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "GroupId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Companies", "Company_Id", "dbo.Companies");
            DropIndex("dbo.Companies", new[] { "Company_Id" });
            DropColumn("dbo.Companies", "Company_Id");
        }
    }
}
