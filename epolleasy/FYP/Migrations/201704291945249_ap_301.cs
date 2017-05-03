namespace FYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ap_301 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "SelectedAnswerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "SelectedAnswerId");
        }
    }
}
