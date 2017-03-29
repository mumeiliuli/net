namespace Emaill.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        UserId = c.String(nullable: false, maxLength: 32),
                        ParentId = c.String(nullable: false, maxLength: 32),
                        Content = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LifeRecords",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        UserId = c.String(nullable: false, maxLength: 32),
                        Content = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RecordLikes",
                c => new
                    {
                        CommentUserId = c.String(nullable: false, maxLength: 32),
                        RecordId = c.String(nullable: false, maxLength: 32),
                        IsCancel = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommentUserId, t.RecordId })
                .ForeignKey("dbo.LifeRecords", t => t.RecordId, cascadeDelete: true)
                .Index(t => t.RecordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecordLikes", "RecordId", "dbo.LifeRecords");
            DropForeignKey("dbo.LifeRecords", "UserId", "dbo.AccountUser");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AccountUser");
            DropIndex("dbo.RecordLikes", new[] { "RecordId" });
            DropIndex("dbo.LifeRecords", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.RecordLikes");
            DropTable("dbo.LifeRecords");
            DropTable("dbo.Comments");
        }
    }
}
