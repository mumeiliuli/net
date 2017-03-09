namespace Emaill.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnote : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Schedules");
            CreateTable(
                "dbo.NoteWords",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        UserId = c.String(maxLength: 32),
                        Content = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountUser", t => t.UserId)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Schedules", "Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Schedules", "Color", c => c.String(maxLength: 10));
            AddPrimaryKey("dbo.Schedules", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoteWords", "UserId", "dbo.AccountUser");
            DropIndex("dbo.NoteWords", new[] { "UserId" });
            DropPrimaryKey("dbo.Schedules");
            AlterColumn("dbo.Schedules", "Color", c => c.String());
            AlterColumn("dbo.Schedules", "Id", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.NoteWords");
            AddPrimaryKey("dbo.Schedules", "Id");
        }
    }
}
