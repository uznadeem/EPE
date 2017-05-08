namespace FYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class May5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "SelectedAnswerId", c => c.Int(nullable: false));
            AlterColumn("dbo.FormUsers", "UserID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FormUsers", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Questions", "SelectedAnswerId");
        }
    }
}
