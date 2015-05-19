using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class EnableOptionalFile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Booking", "FileId", "dbo.File");
            DropIndex("dbo.Booking", new[] { "FileId" });
            AlterColumn("dbo.Booking", "FileId", c => c.Int());
            CreateIndex("dbo.Booking", "FileId");
            AddForeignKey("dbo.Booking", "FileId", "dbo.File", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking", "FileId", "dbo.File");
            DropIndex("dbo.Booking", new[] { "FileId" });
            AlterColumn("dbo.Booking", "FileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Booking", "FileId");
            AddForeignKey("dbo.Booking", "FileId", "dbo.File", "Id", cascadeDelete: true);
        }
    }
}
