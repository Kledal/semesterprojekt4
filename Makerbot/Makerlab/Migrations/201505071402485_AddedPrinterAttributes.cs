namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPrinterAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Printer", "BedTemperature", c => c.Int(nullable: false));
            AddColumn("dbo.Printer", "NozzleTemperature", c => c.Int(nullable: false));
            AddColumn("dbo.Printer", "Printing", c => c.Boolean(nullable: false));
            AddColumn("dbo.Printer", "Paused", c => c.Boolean(nullable: false));
            AddColumn("dbo.Printer", "CurrentLine", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Printer", "CurrentLine");
            DropColumn("dbo.Printer", "Paused");
            DropColumn("dbo.Printer", "Printing");
            DropColumn("dbo.Printer", "NozzleTemperature");
            DropColumn("dbo.Printer", "BedTemperature");
        }
    }
}
