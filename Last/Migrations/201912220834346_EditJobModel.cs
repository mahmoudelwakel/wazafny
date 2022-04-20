namespace Last.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditJobModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.jobs", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.jobs", "UserID");
            AddForeignKey("dbo.jobs", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobs", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.jobs", new[] { "UserID" });
            DropColumn("dbo.jobs", "UserID");
        }
    }
}
