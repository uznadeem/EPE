using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epolleasy.Models
{

    public class User
    {
        public List<object> Claims { get; set; }
        public List<object> Logins { get; set; }
        public List<object> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public object PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public object LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
    }

    public class CommunityUser
    {
        public User User { get; set; }
        public int ID { get; set; }
        public string UserID { get; set; }
        public int CommunityID { get; set; }
    }

    public class Role
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }

    
    public class FormUser
    {
        public User User { get; set; }
        public int ID { get; set; }
        public int QFormID { get; set; }
        public string UserID { get; set; }
    }

    public class Answer
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        public int OptionNo { get; set; }
        public string AnswerStatement { get; set; }
        public int AnsCount { get; set; }
    }

    public class Question
    {
        public List<Answer> Answer { get; set; }
        public int QuestionID { get; set; }
        public string QuestionString { get; set; }
        public int QFormID { get; set; }
        public int SelectedAnswerId { get; set; }
    }

    public class QForms
    {
        public List<FormUser> FormUser { get; set; }
        public List<Question> Question { get; set; }
        public int QFormID { get; set; }
        public string FormDetail { get; set; }
        public string FormTitle { get; set; }
        public string FormOwner { get; set; }
        public int FormType { get; set; }
        public string Creation_Time { get; set; }
        public string Expiry_Time { get; set; }
    }

    public class FormsCommunity
    {
        public QForms QForms { get; set; }
        public int ID { get; set; }
        public int QFormID { get; set; }
        public int CommunityID { get; set; }
    }

    public class Com
    {
        public List<CommunityUser> CommunityUsers { get; set; }
        public List<FormsCommunity> FormsCommunity { get; set; }
        public object PrivacyLevel { get; set; }
        public int CommunityID { get; set; }
        public string CommunityName { get; set; }
        public string CommunityDomain { get; set; }
        public string CommunityAbout { get; set; }
        public string CommunityLogo { get; set; }
        public int PrivacyID { get; set; }
        public string CommunityAdmin { get; set; }
    }



    public class Appuser
    {
        public List<object> Claims { get; set; }
        public List<object> Logins { get; set; }
        public List<Role> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public object PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public object LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
    }

    
    public class Community
    {
        public List<CommunityUser> CommunityUsers { get; set; }
        public List<object> FormsCommunity { get; set; }
        public object PrivacyLevel { get; set; }
        public int CommunityID { get; set; }
        public string CommunityName { get; set; }
        public string CommunityDomain { get; set; }
        public string CommunityAbout { get; set; }
        public string CommunityLogo { get; set; }
        public int PrivacyID { get; set; }
        public string CommunityAdmin { get; set; }
    }

    
    public class ActiveFom
    {
        public Community Community { get; set; }
        public QForms QForms { get; set; }
        public int ID { get; set; }
        public int QFormID { get; set; }
        public int CommunityID { get; set; }
    }

    public class Dashboard
    {
        public List<Com> Com { get; set; }
        public Appuser appuser { get; set; }
        public List<ActiveFom> active_fom { get; set; }
        public List<object> sealed_fom { get; set; }
    }

}
