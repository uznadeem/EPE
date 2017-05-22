namespace FYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fmtypett : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QForms", "FormType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QForms", "FormType");
        }
    }
}
