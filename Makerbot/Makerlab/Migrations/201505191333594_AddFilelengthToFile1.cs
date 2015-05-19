namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilelengthToFile1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.File", "NumberOfLines", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.File", "NumberOfLines", c => c.String());
        }
    }
}
