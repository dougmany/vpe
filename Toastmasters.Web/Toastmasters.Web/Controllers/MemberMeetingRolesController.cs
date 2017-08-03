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
    public class MemberMeetingRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberMeetingRolesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: MemberMeetingRoles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MemberMeetingRoles.Include(m => m.MeetingRole).Include(m => m.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MemberMeetingRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberMeetingRole = await _context.MemberMeetingRoles
                .Include(m => m.MeetingRole)
                .Include(m => m.Member)
                .SingleOrDefaultAsync(m => m.MemberMeetingRoleID == id);
            if (memberMeetingRole == null)
            {
                return NotFound();
            }

            return View(memberMeetingRole);
        }

        // GET: MemberMeetingRoles/Create
        public IActionResult Create()
        {
            ViewData["MeetingRoleID"] = new SelectList(_context.MeetingRoles, "MeetingRoleID", "Name");
            ViewData["MemberID"] = new SelectList(_context.Members, "MemberID", "FullName");
            return View();
        }

        // POST: MemberMeetingRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberMeetingRoleID,MemberID,MeetingRoleID,Meeting,Signoff")] MemberMeetingRole memberMeetingRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberMeetingRole);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MeetingRoleID"] = new SelectList(_context.MeetingRoles, "MeetingRoleID", "Name", memberMeetingRole.MeetingRoleID);
            ViewData["MemberID"] = new SelectList(_context.Members, "MemberID", "FullName", memberMeetingRole.MemberID);
            return View(memberMeetingRole);
        }

        // GET: MemberMeetingRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberMeetingRole = await _context.MemberMeetingRoles.SingleOrDefaultAsync(m => m.MemberMeetingRoleID == id);
            if (memberMeetingRole == null)
            {
                return NotFound();
            }
            ViewData["MeetingRoleID"] = new SelectList(_context.MeetingRoles, "MeetingRoleID", "MeetingRoleID", memberMeetingRole.MeetingRoleID);
            ViewData["MemberID"] = new SelectList(_context.Members, "MemberID", "MemberID", memberMeetingRole.MemberID);
            return View(memberMeetingRole);
        }

        // POST: MemberMeetingRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberMeetingRoleID,MemberID,MeetingRoleID,Meeting,Signoff")] MemberMeetingRole memberMeetingRole)
        {
            if (id != memberMeetingRole.MemberMeetingRoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberMeetingRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberMeetingRoleExists(memberMeetingRole.MemberMeetingRoleID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["MeetingRoleID"] = new SelectList(_context.MeetingRoles, "MeetingRoleID", "MeetingRoleID", memberMeetingRole.MeetingRoleID);
            ViewData["MemberID"] = new SelectList(_context.Members, "MemberID", "MemberID", memberMeetingRole.MemberID);
            return View(memberMeetingRole);
        }

        // GET: MemberMeetingRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberMeetingRole = await _context.MemberMeetingRoles
                .Include(m => m.MeetingRole)
                .Include(m => m.Member)
                .SingleOrDefaultAsync(m => m.MemberMeetingRoleID == id);
            if (memberMeetingRole == null)
            {
                return NotFound();
            }

            return View(memberMeetingRole);
        }

        // POST: MemberMeetingRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberMeetingRole = await _context.MemberMeetingRoles.SingleOrDefaultAsync(m => m.MemberMeetingRoleID == id);
            _context.MemberMeetingRoles.Remove(memberMeetingRole);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MemberMeetingRoleExists(int id)
        {
            return _context.MemberMeetingRoles.Any(e => e.MemberMeetingRoleID == id);
        }
    }
}
