using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class QForm
    {

        public int QFormID { get; set; }

        public string FormDetail { get; set; }

        public string FormTitle { get; set; }
        
        [Required]
        public string FormOwner { get; set; }
        
        [Required]  //get system date
        [DataType(DataType.DateTime)]
        public DateTime Creation_Time { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Expiry_Time { get; set; }
        
    }
}