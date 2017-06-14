using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class Community
    {
        [Key]
        public int CommunityID { get; set; }

        [Required]
        public string CommunityName { get; set; }

       
        public string CommunityDomain { get; set; }

        public string CommunityAbout { get; set; }

        public string CommunityLogo { get; set; }
        
        //privacyLevel Foreign Key
       [Required] 
        public int PrivacyID { get; set; }

        //username of admin as foreign key
        
        public string CommunityAdmin { get; set; }
        
        public virtual PrivacyLevel PrivacyLevel { get; set; }

        public virtual  ICollection<CommunityUser> CommunityUsers { get; set; }

        public virtual ICollection<FormCommunity> FormsCommunity { get; set; }   
     

        public Community()
        {
            FormsCommunity = new Collection<FormCommunity>();
            CommunityUsers = new Collection<CommunityUser>();
        }





    }


}