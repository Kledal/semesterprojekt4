using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class AddedLastSeenToPrinter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Printer", "LastSeen", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Printer", "LastSeen");
        }
    }
}
