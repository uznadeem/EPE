using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel
{
    public class QuestionAnswerViewModel
    {
        public Question Ques { get; set; }

        public Answer Ans { get; set; }

        public int counter { get; set; }
    }
}