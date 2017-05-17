using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel
{
    public class FormResultViewModel
    {

        public IList<Question> Ques { get; set; }
        public int comid { get; set; }
        public int qf_id { get; set; }
    }
}