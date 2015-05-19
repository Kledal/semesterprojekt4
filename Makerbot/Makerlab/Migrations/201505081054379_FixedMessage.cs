using System.Data.Entity.Migrations;

namespace Makerlab.Migrations
{
    public partial class FixedMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Message");
        }
    }
}
