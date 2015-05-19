using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class AddedPrinterCommand1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrinterCommand",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Command = c.String(),
                        Printer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Printer", t => t.Printer_Id)
                .Index(t => t.Printer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrinterCommand", "Printer_Id", "dbo.Printer");
            DropIndex("dbo.PrinterCommand", new[] { "Printer_Id" });
            DropTable("dbo.PrinterCommand");
        }
    }
}
