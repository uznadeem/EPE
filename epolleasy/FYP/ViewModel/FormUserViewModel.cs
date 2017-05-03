using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel
{
    public class FormUserViewModel
    {
        public IList<FormUser> Fu { get; set; }
        public ApplicationUser au { get; set; }
    }
}