using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class FormCommunity
    {
       
        public int ID { get; set; }


        [Required]
        public int QFormID { get; set; }

        [Required]
        public int CommunityID { get; set; }

        public virtual Community Community { get; set; }

        public virtual QForm QForms { get; set; }

    }
}