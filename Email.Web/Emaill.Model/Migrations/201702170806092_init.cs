namespace Emaill.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Account = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 50),
                        LoginTime = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        UserId = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsOpen = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        AlbumId = c.String(nullable: false, maxLength: 32),
                        UploadTime = c.DateTime(nullable: false),
                        Url = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.Albums", "UserId", "dbo.AccountUser");
            DropIndex("dbo.Pictures", new[] { "AlbumId" });
            DropIndex("dbo.Albums", new[] { "UserId" });
            DropTable("dbo.Pictures");
            DropTable("dbo.Albums");
            DropTable("dbo.AccountUser");
        }
    }
}
