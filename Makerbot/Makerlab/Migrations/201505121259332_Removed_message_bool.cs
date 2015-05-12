namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_message_bool : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Message", "Show");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message", "Show", c => c.Boolean(nullable: false));
        }
    }
}
