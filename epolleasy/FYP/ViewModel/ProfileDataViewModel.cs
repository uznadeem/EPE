using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FYP.Models;

namespace FYP.ViewModel
{
    public class ProfileDataViewModel
    {

        public ApplicationUser User { get; set; }
        public List<CommunityUser> CommunitiesList { get; set; }
//        public bool IsMember { get; set; }
//        public Community Community { get; set; }


    }
}