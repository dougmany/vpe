﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models
{
    public class UserViewModel
    {
        public String UserID { get; set; }
        public Int32? MemberID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public Boolean EmailConfirmed { get; set; }
        public String RoleID { get; set; }
    }
}
