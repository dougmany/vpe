using System;
using System.ComponentModel.DataAnnotations;

namespace Toastmasters.Web.Models
{
    public class Club
    {
        public Int32 ClubID { get; set; }
        [Display(Name = "Name")]
        public String ClubName { get; set; }
        [Display(Name = "Number")]
        public String ClubNumber { get; set; }
        public String District { get; set; }
        [Display(Name = "Day")]
        public String MeetingDay { get; set; }
        [Display(Name = "Room")]
        public String MeetingRoom { get; set; }
    }
}
