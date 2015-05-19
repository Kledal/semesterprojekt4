using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class AddedStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.File", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.File", "FilePath");
        }
    }
}
