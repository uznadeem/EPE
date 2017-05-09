using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel
{
    public class QuestionAndAnswerViewModel
    {

        public IList<Question> Quest { get; set; }
        public IList<Answer> Answ {get;set;}
    }
}