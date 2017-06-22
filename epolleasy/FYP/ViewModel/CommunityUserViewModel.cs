using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel
{
    public class CommunityUserViewModel
    {
        public IList<Community> Com  { get; set; }
         public ApplicationUser appuser { get; set; }
        //public IList<FormCommunity> fcom { get; set; }

        public IList<FormCommunity> active_fom { get; set;}

        public IList<FormCommunity> sealed_fom { get; set; }

        public int Tform { get; set; }
        //public IList<QForm> qf { get; set; }

        //public int activeform { get; set; }
        //public int sealedform { get; set; }

    }
}