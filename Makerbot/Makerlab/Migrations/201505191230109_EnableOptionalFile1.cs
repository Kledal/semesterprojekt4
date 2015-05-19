namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableOptionalFile1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Booking", "FileId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Booking", "FileId", c => c.Int(nullable: false));
        }
    }
}
