namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyContactPersonWarehouse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "ContactPersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Warehouses", "ContactPersonId");
            AddForeignKey("dbo.Warehouses", "ContactPersonId", "dbo.ContactPersons", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warehouses", "ContactPersonId", "dbo.ContactPersons");
            DropIndex("dbo.Warehouses", new[] { "ContactPersonId" });
            DropColumn("dbo.Warehouses", "ContactPersonId");
        }
    }
}
