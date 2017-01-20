namespace Emaill.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountUser", "Account", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.AccountUser", "Password", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountUser", "Password", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.AccountUser", "Account", c => c.String(maxLength: 50, storeType: "nvarchar"));
        }
    }
}
