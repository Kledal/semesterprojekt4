namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBookingTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booking", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Booking", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Booking", "EndTime");
            DropColumn("dbo.Booking", "StartTime");
        }
    }
}
