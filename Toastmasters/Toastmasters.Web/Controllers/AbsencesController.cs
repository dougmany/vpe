using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toastmasters.Web.Data;
using Toastmasters.Web.Models;

namespace Toastmasters.Web.Controllers
{
    public class AbsencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbsencesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Absences
        public async Task<IActionResult> Index()
        {
            var members = _context.Members.ToArray();
            var meetings = _context.Meetings.ToArray();

            var model = await _context.Absences.Select(m=> new AbsenceViewModel
            {
                AbsenceID = m.AbsenceID,
                MeetingDate = m.MeetingID.ToString(),
                MemberName= members.Where(me=>me.MemberID == m.MemberID).FirstOrDefault().FullName,
                Role = m.Role
            }).OrderByDescending(a=>a.MeetingDate).ToListAsync();

            foreach (var item in model)
            {
                item.MeetingDate = meetings.Where(m => m.MeetingID.ToString() == item.MeetingDate).FirstOrDefault().MeetingDate.ToString("{}:d");
            }

            return View(model);
        }
    }
}
