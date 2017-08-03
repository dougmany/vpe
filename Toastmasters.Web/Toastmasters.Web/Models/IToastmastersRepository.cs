using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models
{
    public interface IToastmastersRepository
    {
        Member GetMemberByID(Int32 id);

        List<Member> GetMembers();

    }
}
