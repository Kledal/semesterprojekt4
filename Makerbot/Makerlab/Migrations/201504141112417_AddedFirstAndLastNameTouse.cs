using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class AddedFirstAndLastNameTouse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "FirstName", c => c.String());
            AddColumn("dbo.User", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "LastName");
            DropColumn("dbo.User", "FirstName");
        }
    }
}
