namespace FYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class April13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "QForm_QFormID", "dbo.QForms");
            DropForeignKey("dbo.FormCommunities", "QForms_QFormID", "dbo.QForms");
            DropForeignKey("dbo.FormUsers", "QForm_QFormID", "dbo.QForms");
            DropIndex("dbo.Questions", new[] { "QForm_QFormID" });
            DropIndex("dbo.FormCommunities", new[] { "QForms_QFormID" });
            DropIndex("dbo.FormUsers", new[] { "QForm_QFormID" });
            RenameColumn(table: "dbo.Questions", name: "QForm_QFormID", newName: "QFormID");
            RenameColumn(table: "dbo.FormCommunities", name: "QForms_QFormID", newName: "QFormID");
            RenameColumn(table: "dbo.FormUsers", name: "QForm_QFormID", newName: "QFormID");
            AddColumn("dbo.Answers", "AnsCount", c => c.Int(nullable: false));
            AddColumn("dbo.QForms", "FormTitle", c => c.String());
            AddColumn("dbo.QForms", "Creation_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Communities", "AppUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Answers", "AnswerStatement", c => c.String());
            AlterColumn("dbo.Questions", "QFormID", c => c.Int(nullable: false));
            AlterColumn("dbo.FormCommunities", "QFormID", c => c.Int(nullable: false));
            AlterColumn("dbo.FormUsers", "QFormID", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "QFormID");
            CreateIndex("dbo.Communities", "AppUser_Id");
            CreateIndex("dbo.FormCommunities", "QFormID");
            CreateIndex("dbo.FormUsers", "QFormID");
            AddForeignKey("dbo.Communities", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Questions", "QFormID", "dbo.QForms", "QFormID", cascadeDelete: true);
            AddForeignKey("dbo.FormCommunities", "QFormID", "dbo.QForms", "QFormID", cascadeDelete: true);
            AddForeignKey("dbo.FormUsers", "QFormID", "dbo.QForms", "QFormID", cascadeDelete: true);
            DropColumn("dbo.Questions", "FormID");
            DropColumn("dbo.QForms", "Created_date");
            DropColumn("dbo.FormCommunities", "FormID");
            DropColumn("dbo.FormUsers", "FormID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FormUsers", "FormID", c => c.Int(nullable: false));
            AddColumn("dbo.FormCommunities", "FormID", c => c.Int(nullable: false));
            AddColumn("dbo.QForms", "Created_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "FormID", c => c.Int(nullable: false));
            DropForeignKey("dbo.FormUsers", "QFormID", "dbo.QForms");
            DropForeignKey("dbo.FormCommunities", "QFormID", "dbo.QForms");
            DropForeignKey("dbo.Questions", "QFormID", "dbo.QForms");
            DropForeignKey("dbo.Communities", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FormUsers", new[] { "QFormID" });
            DropIndex("dbo.FormCommunities", new[] { "QFormID" });
            DropIndex("dbo.Communities", new[] { "AppUser_Id" });
            DropIndex("dbo.Questions", new[] { "QFormID" });
            AlterColumn("dbo.FormUsers", "QFormID", c => c.Int());
            AlterColumn("dbo.FormCommunities", "QFormID", c => c.Int());
            AlterColumn("dbo.Questions", "QFormID", c => c.Int());
            AlterColumn("dbo.Answers", "AnswerStatement", c => c.String(nullable: false));
            DropColumn("dbo.Communities", "AppUser_Id");
            DropColumn("dbo.QForms", "Creation_Time");
            DropColumn("dbo.QForms", "FormTitle");
            DropColumn("dbo.Answers", "AnsCount");
            RenameColumn(table: "dbo.FormUsers", name: "QFormID", newName: "QForm_QFormID");
            RenameColumn(table: "dbo.FormCommunities", name: "QFormID", newName: "QForms_QFormID");
            RenameColumn(table: "dbo.Questions", name: "QFormID", newName: "QForm_QFormID");
            CreateIndex("dbo.FormUsers", "QForm_QFormID");
            CreateIndex("dbo.FormCommunities", "QForms_QFormID");
            CreateIndex("dbo.Questions", "QForm_QFormID");
            AddForeignKey("dbo.FormUsers", "QForm_QFormID", "dbo.QForms", "QFormID");
            AddForeignKey("dbo.FormCommunities", "QForms_QFormID", "dbo.QForms", "QFormID");
            AddForeignKey("dbo.Questions", "QForm_QFormID", "dbo.QForms", "QFormID");
        }
    }
}
