namespace FYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class May28_Uzair : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        QuestionID = c.Int(nullable: false),
                        OptionNo = c.Int(nullable: false),
                        AnswerStatement = c.String(),
                        AnsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Communities",
                c => new
                    {
                        CommunityID = c.Int(nullable: false, identity: true),
                        CommunityName = c.String(nullable: false),
                        CommunityDomain = c.String(nullable: false),
                        CommunityAbout = c.String(),
                        CommunityLogo = c.String(),
                        PrivacyID = c.Int(nullable: false),
                        CommunityAdmin = c.String(),
                        PrivacyLevel_PrivacyLevelID = c.Int(),
                    })
                .PrimaryKey(t => t.CommunityID)
                .ForeignKey("dbo.PrivacyLevels", t => t.PrivacyLevel_PrivacyLevelID)
                .Index(t => t.PrivacyLevel_PrivacyLevelID);
            
            CreateTable(
                "dbo.CommunityUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 128),
                        CommunityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Communities", t => t.CommunityID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CommunityID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserRole = c.String(),
                        Gender = c.String(),
                        DOB = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.FormCommunities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QFormID = c.Int(nullable: false),
                        CommunityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Communities", t => t.CommunityID, cascadeDelete: true)
                .ForeignKey("dbo.QForms", t => t.QFormID, cascadeDelete: true)
                .Index(t => t.QFormID)
                .Index(t => t.CommunityID);
            
            CreateTable(
                "dbo.QForms",
                c => new
                    {
                        QFormID = c.Int(nullable: false, identity: true),
                        FormDetail = c.String(),
                        FormTitle = c.String(),
                        FormOwner = c.String(nullable: false),
                        FormType = c.Int(nullable: false),
                        Creation_Time = c.DateTime(nullable: false),
                        Expiry_Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QFormID);
            
            CreateTable(
                "dbo.FormUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QFormID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QForms", t => t.QFormID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.QFormID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        QuestionString = c.String(nullable: false),
                        QFormID = c.Int(nullable: false),
                        SelectedAnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.QForms", t => t.QFormID, cascadeDelete: true)
                .Index(t => t.QFormID);
            
            CreateTable(
                "dbo.PrivacyLevels",
                c => new
                    {
                        PrivacyLevelID = c.Int(nullable: false, identity: true),
                        PrivacySettingName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PrivacyLevelID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Communities", "PrivacyLevel_PrivacyLevelID", "dbo.PrivacyLevels");
            DropForeignKey("dbo.FormCommunities", "QFormID", "dbo.QForms");
            DropForeignKey("dbo.Questions", "QFormID", "dbo.QForms");
            DropForeignKey("dbo.Answers", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.FormUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormUsers", "QFormID", "dbo.QForms");
            DropForeignKey("dbo.FormCommunities", "CommunityID", "dbo.Communities");
            DropForeignKey("dbo.CommunityUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommunityUsers", "CommunityID", "dbo.Communities");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Questions", new[] { "QFormID" });
            DropIndex("dbo.FormUsers", new[] { "UserID" });
            DropIndex("dbo.FormUsers", new[] { "QFormID" });
            DropIndex("dbo.FormCommunities", new[] { "CommunityID" });
            DropIndex("dbo.FormCommunities", new[] { "QFormID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CommunityUsers", new[] { "CommunityID" });
            DropIndex("dbo.CommunityUsers", new[] { "UserID" });
            DropIndex("dbo.Communities", new[] { "PrivacyLevel_PrivacyLevelID" });
            DropIndex("dbo.Answers", new[] { "QuestionID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PrivacyLevels");
            DropTable("dbo.Questions");
            DropTable("dbo.FormUsers");
            DropTable("dbo.QForms");
            DropTable("dbo.FormCommunities");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CommunityUsers");
            DropTable("dbo.Communities");
            DropTable("dbo.Answers");
        }
    }
}
