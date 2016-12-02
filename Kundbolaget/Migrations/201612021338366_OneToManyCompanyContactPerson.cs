namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyCompanyContactPerson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Companies", "ContactPersonId");
            AddForeignKey("dbo.Companies", "ContactPersonId", "dbo.ContactPersons", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "ContactPersonId", "dbo.ContactPersons");
            DropIndex("dbo.Companies", new[] { "ContactPersonId" });
            DropTable("dbo.ContactPersons");
        }
    }
}
