using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.ViewModel.ParticipantApi
{
    public class PApiFormParticipateViewModel
    {
        public int fid { get; set; }

        public int cid { get; set; }

        public PFormViewModel pfm { get; set; }

        public PApiFormParticipateViewModel()
        { }

    }
}