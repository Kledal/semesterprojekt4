namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAttributeToUserRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRole", "CanViewDashboard", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRole", "CanViewDashboard");
        }
    }
}
