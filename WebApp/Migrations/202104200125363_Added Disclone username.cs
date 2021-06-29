namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDisclonesuername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DiscloneUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DiscloneUserName");
        }
    }
}
