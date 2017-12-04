using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models
{
    public class Absence
    {
        public Int32 AbsenceID { get; set; }
        public Int32 MemberID { get; set; }
        public Int32 MeetingID { get; set; }
        public MeetingRoleNames Role { get; set; }
    }
}
