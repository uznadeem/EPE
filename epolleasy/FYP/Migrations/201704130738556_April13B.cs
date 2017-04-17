namespace FYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class April13B : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Communities", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Communities", new[] { "AppUser_Id" });
            DropColumn("dbo.Communities", "AppUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Communities", "AppUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Communities", "AppUser_Id");
            AddForeignKey("dbo.Communities", "AppUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
