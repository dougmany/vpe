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
    public class MeetingRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingRolesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: MeetingRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.MeetingRoles.ToListAsync());
        }

        // GET: MeetingRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingRole = await _context.MeetingRoles
                .SingleOrDefaultAsync(m => m.MeetingRoleID == id);
            if (meetingRole == null)
            {
                return NotFound();
            }

            return View(meetingRole);
        }

        // GET: MeetingRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeetingRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeetingRoleID,Name,Description")] MeetingRole meetingRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetingRole);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(meetingRole);
        }

        // GET: MeetingRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingRole = await _context.MeetingRoles.SingleOrDefaultAsync(m => m.MeetingRoleID == id);
            if (meetingRole == null)
            {
                return NotFound();
            }
            return View(meetingRole);
        }

        // POST: MeetingRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeetingRoleID,Name,Description")] MeetingRole meetingRole)
        {
            if (id != meetingRole.MeetingRoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingRoleExists(meetingRole.MeetingRoleID))
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
            return View(meetingRole);
        }

        // GET: MeetingRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingRole = await _context.MeetingRoles
                .SingleOrDefaultAsync(m => m.MeetingRoleID == id);
            if (meetingRole == null)
            {
                return NotFound();
            }

            return View(meetingRole);
        }

        // POST: MeetingRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meetingRole = await _context.MeetingRoles.SingleOrDefaultAsync(m => m.MeetingRoleID == id);
            _context.MeetingRoles.Remove(meetingRole);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MeetingRoleExists(int id)
        {
            return _context.MeetingRoles.Any(e => e.MeetingRoleID == id);
        }
    }
}
