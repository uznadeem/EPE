namespace FYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ap_302 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FormUsers", "UserID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FormUsers", "UserID", c => c.Int(nullable: false));
        }
    }
}
