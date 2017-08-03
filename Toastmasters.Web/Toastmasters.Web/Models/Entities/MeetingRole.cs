using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models
{
    public class MeetingRole
    {
        [Display(Name="Role")]
        public Int32 MeetingRoleID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
