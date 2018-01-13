using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models
{
    public class Speech
    {
        public Int32 SpeechID { get; set; }
        public Int32 MemberID { get; set; }
        public String Title { get; set; }
        public String Project { get; set; }
        [Display(Name ="Time Constraints")]
        public String TimeConstraints { get; set; }
    }
}
