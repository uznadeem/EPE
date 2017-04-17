using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }

        //This is a foreign key
        [Required]
        public int QuestionID { get; set; }
        
        public int OptionNo { get; set; }   //this is a composite primary key with QuestionID

        public string AnswerStatement { get; set; }

        public int AnsCount { get; set; }

        public virtual Question Question { get; set; }


    }
}