﻿using System;
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
        public int UserID { get; set; }

        public virtual ApplicationUser AppUser { get; set; }

        public virtual QForm QForm { get; set; }


    }
}