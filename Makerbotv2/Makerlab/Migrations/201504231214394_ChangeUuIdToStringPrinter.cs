namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUuIdToStringPrinter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Printer", "UuId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Printer", "UuId");
        }
    }
}
