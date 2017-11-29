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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Toastmasters.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public IActionResult Index()
        {
            var model = _context.ApplicationUser
                .Include(a => a.Member)
                .Include(a => a.Roles)
                .Select(a => new UserViewModel
                {
                    Name = a.UserName,
                    UserID = a.Id,
                    Email = a.Email,
                    EmailConfirmed = a.EmailConfirmed,
                    MemberID = a.MemberID,
                    RoleID = a.Roles.FirstOrDefault().RoleId
                }).ToArray();

            return View(model);
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
            var memberList = new SelectList(members, "MemberID", "FirstInitial");
            ViewBag.Members = memberList.Prepend(new SelectListItem { Text = "-Select-", Value = "" });

            var roles = _context.Roles.ToArray();
            var roleList = new SelectList(roles, "Id", "Name");
            ViewBag.Roles = roleList.Prepend(new SelectListItem { Text = "-Select-", Value = "" });

            var model = new UserViewModel
            {
                UserID = applicationUser.Id,
                Name = applicationUser.UserName,
                Email = applicationUser.Email,
                EmailConfirmed = applicationUser.EmailConfirmed,
                MemberID = applicationUser.MemberID
            };
            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == model.UserID);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.MemberID = model.MemberID;
                    user.Email = model.Email;
                    user.UserName = model.Name;
                    user.EmailConfirmed = model.EmailConfirmed;
                    if (model.RoleID != "")
                    {
                        IdentityRole<String> role = _context.Roles.Where(r => r.Id == model.RoleID).FirstOrDefault();
                        if (role != null)
                        {
                            var userRole = _context.UserRoles.Where(ur => ur.RoleId == role.Id && ur.UserId == user.Id).FirstOrDefault();
                            if (userRole == null)
                            {
                                _context.UserRoles.Add(new IdentityUserRole<String> { RoleId = role.Id, UserId = user.Id });
                                _context.SaveChanges();
                            }
                        }
                    }

                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(model.UserID))
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
            var memberList = new SelectList(members, "MemberID", "FirstInitial");
            ViewBag.Members = memberList.Prepend(new SelectListItem { Text = "-Select-", Value = "" });

            var roles = _context.Roles.ToArray();
            var roleList = new SelectList(roles, "Id", "Name");
            ViewBag.Roles = roleList.Prepend(new SelectListItem { Text = "-Select-", Value = "" });

            return View(model);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveUserRole(String userID, String roleID)
        {
            var user = _context.ApplicationUser.Where(u => u.Id == userID).FirstOrDefault();
            var role = _context.Roles.Where(r => r.Id == roleID).FirstOrDefault();
            if (user != null && role != null)
            {
                var userRole = _context.UserRoles.Where(ur => ur.RoleId == roleID && ur.UserId == userID).FirstOrDefault();
                _context.Remove(userRole);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
