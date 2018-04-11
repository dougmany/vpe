using System;

namespace Toastmasters.Web.Models
{
    public class AbsenceViewModel
    {
        public Int32 AbsenceID { get; set; }
        public String MemberName { get; set; }
        public String MeetingDate { get; set; }
        public MeetingRoleNames Role { get; set; }
    }
}
