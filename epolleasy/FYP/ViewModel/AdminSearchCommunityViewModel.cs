using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel
{
    public class AdminSearchCommunityViewModel
    {
        public IEnumerable<Community> com{ get; set; }
        public IEnumerable<Community> searchstr { get; set; }
        public int a { get; set; }
    }
}