namespace Emaill.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updaterecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.LifeRecords", "UserName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LifeRecords", "UserName");
            DropColumn("dbo.Comments", "UserName");
        }
    }
}
