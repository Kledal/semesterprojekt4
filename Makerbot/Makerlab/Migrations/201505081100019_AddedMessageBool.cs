namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMessageBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Message", "Show", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Message", "Show");
        }
    }
}
