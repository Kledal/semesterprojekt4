namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUuIdToStringPrinter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Printer", "UuId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Printer", "UuId", c => c.Int(nullable: false));
        }
    }
}
