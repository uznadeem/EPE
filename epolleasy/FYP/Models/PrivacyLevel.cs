using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class PrivacyLevel
    {

        public int PrivacyLevelID { get; set; }

        [Required]
        public string PrivacySettingName { get; set; }


    }
}