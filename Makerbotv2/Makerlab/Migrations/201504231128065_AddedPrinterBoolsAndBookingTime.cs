namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPrinterBoolsAndBookingTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Printer", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Printer", "UnderMantenance", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Printer", "UnderMantenance");
            DropColumn("dbo.Printer", "Active");
        }
    }
}
