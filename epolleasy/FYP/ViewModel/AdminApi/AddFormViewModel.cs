using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel.AdminApi
{
    public class AddFormViewModel
    {

        public int cid { get; set; }
        public QForm qfom { get; set; }
        public int ft { get; set; }
        public ChkvmViewModel chk { get; set; }
        public AddFormViewModel()
        {

        }
    }
}