namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilelengthToFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.File", "NumberOfLines", c => c.String());
            DropColumn("dbo.File", "FileBytes");
            DropColumn("dbo.File", "ContentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.File", "ContentType", c => c.String());
            AddColumn("dbo.File", "FileBytes", c => c.Binary());
            DropColumn("dbo.File", "NumberOfLines");
        }
    }
}
