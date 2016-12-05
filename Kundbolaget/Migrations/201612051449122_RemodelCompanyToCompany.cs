namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemodelCompanyToCompany : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Companies", name: "Company_Id", newName: "ParentCompanyId");
            RenameIndex(table: "dbo.Companies", name: "IX_Company_Id", newName: "IX_ParentCompanyId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Companies", name: "IX_ParentCompanyId", newName: "IX_Company_Id");
            RenameColumn(table: "dbo.Companies", name: "ParentCompanyId", newName: "Company_Id");
        }
    }
}
