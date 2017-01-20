namespace Emaill.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountUser", "UserName", c => c.String(maxLength: 50, storeType: "nvarchar"));
            DropColumn("dbo.AccountUser", "NickName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountUser", "NickName", c => c.String(maxLength: 50, storeType: "nvarchar"));
            DropColumn("dbo.AccountUser", "UserName");
        }
    }
}
