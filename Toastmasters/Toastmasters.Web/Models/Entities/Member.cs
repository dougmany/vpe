using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models
{
    public class Member
    {
        [Display(Name = "Member")]
        public Int32 MemberID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public Boolean IsPresident { get; set; }
        public Boolean IsSargent { get; set; }
        public Boolean IsActive { get; set; }

        [Display(Name="Name")]
        [NotMapped]
        public String FullName { get { return $"{FirstName} {LastName}"; } }
        [Display(Name = "Name")]
        [NotMapped]
        public String FirstInitial
        {
            get
            {
                if (LastName!= null )
                {
                    return $"{FirstName} {LastName[0]}.";
                }
                return "";
            }
        }
    }
}
