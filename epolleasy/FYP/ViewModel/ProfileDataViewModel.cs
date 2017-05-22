using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FYP.Models;

namespace FYP.ViewModel
{
    public class ProfileDataViewModel
    {

        public ApplicationUser UserT { get; set; }
        public List<CommunityUser> CommunitiesList { get; set; }
        public List<FormCommunity> fc { get; set; }
        
    }
    
}