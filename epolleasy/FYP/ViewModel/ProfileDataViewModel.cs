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
        public List<Community> CommunitiesList { get; set; }
        public List<FormCommunity> fc { get; set; }
        public IList<FormCommunity> activeform { get; set; }
        //public int sealedform { get; set; }
        public IList<FormCommunity> sealedform { get; set; }
        public IList<Community> public_com { get; set; }
        public int com_count { get; set; }
    }


}