namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredBookingToFile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Booking", "FileId", "dbo.File");
            DropIndex("dbo.Booking", new[] { "FileId" });
            //DropColumn("dbo.File", "Id");
            //RenameColumn(table: "dbo.File", name: "FileId", newName: "Id");
            DropPrimaryKey("dbo.File");
            AddColumn("dbo.File", "BookingId", c => c.Int(nullable: false));
            AlterColumn("dbo.File", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.File", "Id");
            CreateIndex("dbo.File", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.File", new[] { "Id" });
            DropPrimaryKey("dbo.File");
            AlterColumn("dbo.File", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.File", "BookingId");
            AddPrimaryKey("dbo.File", "Id");
            //RenameColumn(table: "dbo.File", name: "Id", newName: "FileId");
            //AddColumn("dbo.File", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Booking", "FileId");
            AddForeignKey("dbo.Booking", "FileId", "dbo.File", "Id");
        }
    }
}
