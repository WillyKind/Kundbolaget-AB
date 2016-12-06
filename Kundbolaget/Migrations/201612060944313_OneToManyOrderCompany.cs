namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyOrderCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CompanyId");
            AddForeignKey("dbo.Orders", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Orders", new[] { "CompanyId" });
            DropColumn("dbo.Orders", "CompanyId");
        }
    }
}
