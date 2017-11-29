using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toastmasters.Web.Data;
using Toastmasters.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Toastmasters.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUser.Include(a=>a.Member).ToListAsync());
        }        

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            var members = _context.Members.Where(m => m.IsActive).ToArray();
            ViewBag.Members = new SelectList(members, "MemberID", "FirstInitial");

            var roles = _context.Roles.ToArray();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View(applicationUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == applicationUser.Id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.MemberID = applicationUser.MemberID;
                    user.Email = applicationUser.Email;
                    user.UserName = applicationUser.UserName;
                    user.EmailConfirmed = applicationUser.EmailConfirmed;
                    if (applicationUser.Roles.FirstOrDefault() != null)
                    {
                        user.Roles.Add(applicationUser.Roles.First());
                    }

                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
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

            var members = _context.Members.Where(m => m.IsActive).ToArray();
            ViewBag.Members = new SelectList(members, "MemberID", "FirstInitial");

            return View(applicationUser);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
