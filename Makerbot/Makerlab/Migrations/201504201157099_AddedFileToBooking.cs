namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileToBooking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Booking", "File_Id", c => c.Int());
            CreateIndex("dbo.Booking", "File_Id");
            AddForeignKey("dbo.Booking", "File_Id", "dbo.File", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking", "File_Id", "dbo.File");
            DropIndex("dbo.Booking", new[] { "File_Id" });
            DropColumn("dbo.Booking", "File_Id");
            DropTable("dbo.File");
        }
    }
}
