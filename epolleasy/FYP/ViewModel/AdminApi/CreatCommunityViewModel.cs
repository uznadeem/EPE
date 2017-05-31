using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel.AdminApi
{
    public class CreatCommunityViewModel
    {
        public Community com { get; set; }
        public HttpPostedFileBase  file{ get; set; }
        public CreatCommunityViewModel()
        { }
    }
}