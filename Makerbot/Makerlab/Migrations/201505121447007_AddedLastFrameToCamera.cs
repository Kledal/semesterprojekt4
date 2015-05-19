using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class AddedLastFrameToCamera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Printer", "LastFrame", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Printer", "LastFrame");
        }
    }
}
