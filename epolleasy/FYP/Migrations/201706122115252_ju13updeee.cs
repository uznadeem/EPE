namespace FYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ju13updeee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Communities", "CommunityDomain", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Communities", "CommunityDomain", c => c.String(nullable: false));
        }
    }
}
