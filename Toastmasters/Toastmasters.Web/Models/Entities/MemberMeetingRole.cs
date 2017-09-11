using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models
{
    public class MemberMeetingRole
    {
        public Int32 MemberMeetingRoleID { get; set; }
        public DateTime MeetingDate { get; set; }
        public Int32 MemberID { get; set; }
        public Member Member { get; set; }
        public Int32 MeetingRoleID { get; set; }

        [Display(Name = "Meeting Role")]
        public MeetingRole MeetingRole { get; set; }

        [Display(Name ="Meeting Date")]
        [DisplayFormat(DataFormatString ="d")]
       
        public Boolean Signoff { get; set; }
    }
}
