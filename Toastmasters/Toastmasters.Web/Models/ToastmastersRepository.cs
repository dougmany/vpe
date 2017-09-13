using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models
{
    public class ToastmastersRepository : IToastmastersRepository
    {
        public Member GetMemberByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetMembers()
        {
            throw new NotImplementedException();
        }
    }
}
