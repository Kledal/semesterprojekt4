using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class AddedBookingPrinterIdForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Booking", "Printer_Id", "dbo.Printer");
            DropIndex("dbo.Booking", new[] { "Printer_Id" });
            RenameColumn(table: "dbo.Booking", name: "Printer_Id", newName: "PrinterId");
            AlterColumn("dbo.Booking", "PrinterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Booking", "PrinterId");
            AddForeignKey("dbo.Booking", "PrinterId", "dbo.Printer", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking", "PrinterId", "dbo.Printer");
            DropIndex("dbo.Booking", new[] { "PrinterId" });
            AlterColumn("dbo.Booking", "PrinterId", c => c.Int());
            RenameColumn(table: "dbo.Booking", name: "PrinterId", newName: "Printer_Id");
            CreateIndex("dbo.Booking", "Printer_Id");
            AddForeignKey("dbo.Booking", "Printer_Id", "dbo.Printer", "Id");
        }
    }
}
