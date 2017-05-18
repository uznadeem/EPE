using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class CommunityUser
    {

        public int ID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public int CommunityID { get; set; }

        public virtual Community Community { get; set; }
        public virtual ApplicationUser User { get; set; }
        //public virtual ICollection<Community> CommunityCollection { get; set; }



    }
}