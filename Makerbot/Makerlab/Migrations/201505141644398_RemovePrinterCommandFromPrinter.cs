namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePrinterCommandFromPrinter : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrinterCommand", "Printer_Id", "dbo.Printer");
            DropIndex("dbo.PrinterCommand", new[] { "Printer_Id" });
            DropTable("dbo.PrinterCommand");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PrinterCommand",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Command = c.String(),
                        Printer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.PrinterCommand", "Printer_Id");
            AddForeignKey("dbo.PrinterCommand", "Printer_Id", "dbo.Printer", "Id");
        }
    }
}
