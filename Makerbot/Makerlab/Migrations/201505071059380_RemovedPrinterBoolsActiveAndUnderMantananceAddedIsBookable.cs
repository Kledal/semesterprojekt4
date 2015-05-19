using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class RemovedPrinterBoolsActiveAndUnderMantananceAddedIsBookable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Printer", "IsBookable", c => c.Boolean(nullable: false));
            DropColumn("dbo.Printer", "Active");
            DropColumn("dbo.Printer", "UnderMantenance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Printer", "UnderMantenance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Printer", "Active", c => c.Boolean(nullable: false));
            DropColumn("dbo.Printer", "IsBookable");
        }
    }
}
