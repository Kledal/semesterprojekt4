using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class TonyTilfoejet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.File", "FileBytes", c => c.Binary());
            AddColumn("dbo.File", "ContentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.File", "ContentType");
            DropColumn("dbo.File", "FileBytes");
        }
    }
}
