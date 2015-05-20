namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileDontHaveBooking : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.File", "Id", "dbo.Booking");
            DropIndex("dbo.File", new[] { "Id" });
            DropPrimaryKey("dbo.File");
            AlterColumn("dbo.File", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.File", "Id");
            CreateIndex("dbo.Booking", "FileId");
            AddForeignKey("dbo.Booking", "FileId", "dbo.File", "Id");
            DropColumn("dbo.File", "BookingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.File", "BookingId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Booking", "FileId", "dbo.File");
            DropIndex("dbo.Booking", new[] { "FileId" });
            DropPrimaryKey("dbo.File");
            AlterColumn("dbo.File", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.File", "Id");
            CreateIndex("dbo.File", "Id");
            AddForeignKey("dbo.File", "Id", "dbo.Booking", "Id");
        }
    }
}
