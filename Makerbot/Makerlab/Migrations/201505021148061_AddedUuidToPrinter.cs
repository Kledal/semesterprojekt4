using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class AddedUuidToPrinter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Printer", "UuId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Printer", "UuId");
        }
    }
}
