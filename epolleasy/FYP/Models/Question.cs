using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class Question
    {

        public int QuestionID { get; set; }

        [Required]
        public string QuestionString { get; set; }

        //This is a foreign key
        [Required]
        public int QFormID { get; set; }

        public virtual QForm QForm { get; set; }



    }
}