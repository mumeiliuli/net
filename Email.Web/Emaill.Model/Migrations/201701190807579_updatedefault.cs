namespace Emaill.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedefault : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountUser", "UserName", c => c.String(nullable: false, maxLength: 50,storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountUser", "UserName", c => c.String(maxLength: 50, storeType: "nvarchar"));
        }
    }
}
