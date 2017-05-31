using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel.AdminApi
{
    public class AAddQuestionViewModel
    {
        public int cid { get; set; }
        public string[] Mul_ans { get; set; }
        public Question ques { get; set; }
        public int qfid { get; set; }
        public int ft { get; set; }

        public AAddQuestionViewModel()
        { }
    }
}