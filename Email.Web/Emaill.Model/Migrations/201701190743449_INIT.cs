namespace Emaill.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INIT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Account = c.String(maxLength: 50, storeType: "nvarchar"),
                        Password = c.String(maxLength: 50, storeType: "nvarchar"),
                        NickName = c.String(maxLength: 50, storeType: "nvarchar"),
                        LoginTime = c.DateTime(nullable: false, precision: 0),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountUser");
        }
    }
}
