using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class FormUser
    {
        public int ID { get; set; }


        [Required]
        public int QFormID { get; set; }

        [Required]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual QForm QForm { get; set; }


    }
}