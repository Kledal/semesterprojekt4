using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingPrintError",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        ErrorType = c.String(),
                        Booking_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.Booking_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Booking_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Printer_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Printer", t => t.Printer_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Printer_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Printer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudieNummer = c.Int(nullable: false),
                        AccessCard = c.Int(nullable: false),
                        UserRoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRole", t => t.UserRoleId)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        CanCreateBooking = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole");
            DropForeignKey("dbo.Booking", "User_Id", "dbo.User");
            DropForeignKey("dbo.BookingPrintError", "User_Id", "dbo.User");
            DropForeignKey("dbo.Booking", "Printer_Id", "dbo.Printer");
            DropForeignKey("dbo.BookingPrintError", "Booking_Id", "dbo.Booking");
            DropIndex("dbo.User", new[] { "UserRoleId" });
            DropIndex("dbo.Booking", new[] { "User_Id" });
            DropIndex("dbo.Booking", new[] { "Printer_Id" });
            DropIndex("dbo.BookingPrintError", new[] { "User_Id" });
            DropIndex("dbo.BookingPrintError", new[] { "Booking_Id" });
            DropTable("dbo.UserRole");
            DropTable("dbo.User");
            DropTable("dbo.Printer");
            DropTable("dbo.Booking");
            DropTable("dbo.BookingPrintError");
        }
    }
}
