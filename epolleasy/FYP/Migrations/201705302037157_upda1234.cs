namespace FYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upda1234 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QForms", "FormTitle", c => c.String(nullable: false));
            AlterColumn("dbo.QForms", "FormOwner", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QForms", "FormOwner", c => c.String(nullable: false));
            AlterColumn("dbo.QForms", "FormTitle", c => c.String());
        }
    }
}
