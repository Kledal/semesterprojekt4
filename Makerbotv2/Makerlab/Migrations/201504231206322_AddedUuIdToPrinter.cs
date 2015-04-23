namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUuIdToPrinter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Printer", "UuId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Printer", "UuId");
        }
    }
}
