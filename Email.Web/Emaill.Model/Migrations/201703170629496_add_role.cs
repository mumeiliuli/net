namespace Emaill.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_role : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AccountUser", "RoleId", c => c.Int());
            CreateIndex("dbo.AccountUser", "RoleId");
            AddForeignKey("dbo.AccountUser", "RoleId", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountUser", "RoleId", "dbo.Roles");
            DropIndex("dbo.AccountUser", new[] { "RoleId" });
            DropColumn("dbo.AccountUser", "RoleId");
            DropTable("dbo.Roles");
        }
    }
}
