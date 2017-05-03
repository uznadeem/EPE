using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FYP.Models;

namespace FYP.ViewModel
{
    public class ViewDetailViewModel
    {
        public Community comm { get; set; }
        public IEnumerable<FormCommunity> fcom { get; set; }
        public IList<CommunityUser> c_usr { get; set; }
        public IEnumerable<ApplicationUser> Au { get; set; }
    }
}