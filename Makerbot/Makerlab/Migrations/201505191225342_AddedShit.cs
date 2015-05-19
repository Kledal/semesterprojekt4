namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Booking", "FileId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Booking", "FileId", c => c.Int());
        }
    }
}
