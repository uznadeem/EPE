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

        [Required]
        public string FormTitle { get; set; }
        
        public string FormOwner { get; set; }

        public int FormType { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime Creation_Time { get; set; }

        [Required]
 
        [DataType(DataType.DateTime)]
        public DateTime Expiry_Time { get; set; }
        
        public virtual ICollection<Question> Question { get; set; }

        public virtual ICollection<FormUser> FormUser { get; set; }

    }
}