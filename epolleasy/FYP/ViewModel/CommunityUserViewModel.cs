﻿using FYP.Models;
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
    }
}