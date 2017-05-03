using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel
{
    public class PFormViewModel
    {
        //public IQueryable<IEnumerable<char>> Ques { get; set; }
        
            public IList<Question> Ques { get; set; }
            //public IEnumerable<Answer> Ans { get; set; }
            //public int Selected_Answer { get; set; }
        //public int counter { get; set; }
    }
}