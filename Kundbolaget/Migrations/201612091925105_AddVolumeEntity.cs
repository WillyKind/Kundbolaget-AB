namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVolumeEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Volumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Milliliter = c.Int(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductInfoes", "VolumeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductInfoes", "VolumeId");
            AddForeignKey("dbo.ProductInfoes", "VolumeId", "dbo.Volumes", "Id", cascadeDelete: true);
            DropColumn("dbo.Containers", "Volume");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Containers", "Volume", c => c.Double(nullable: false));
            DropForeignKey("dbo.ProductInfoes", "VolumeId", "dbo.Volumes");
            DropIndex("dbo.ProductInfoes", new[] { "VolumeId" });
            DropColumn("dbo.ProductInfoes", "VolumeId");
            DropTable("dbo.Volumes");
        }
    }
}
