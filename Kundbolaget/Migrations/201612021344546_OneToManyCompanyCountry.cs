namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyCompanyCountry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CountryCode = c.String(nullable: false),
                        Region = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Companies", "CountryId");
            AddForeignKey("dbo.Companies", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "CountryId", "dbo.Countries");
            DropIndex("dbo.Companies", new[] { "CountryId" });
            DropTable("dbo.Countries");
        }
    }
}
