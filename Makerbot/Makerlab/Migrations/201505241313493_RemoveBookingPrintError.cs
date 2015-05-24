namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBookingPrintError : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookingPrintError", "Booking_Id", "dbo.Booking");
            DropForeignKey("dbo.BookingPrintError", "User_Id", "dbo.User");
            DropIndex("dbo.BookingPrintError", new[] { "Booking_Id" });
            DropIndex("dbo.BookingPrintError", new[] { "User_Id" });
            DropTable("dbo.BookingPrintError");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.BookingPrintError", "User_Id");
            CreateIndex("dbo.BookingPrintError", "Booking_Id");
            AddForeignKey("dbo.BookingPrintError", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.BookingPrintError", "Booking_Id", "dbo.Booking", "Id");
        }
    }
}
