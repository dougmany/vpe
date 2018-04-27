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
    public class SpeechesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUserManager _userManager;

        public SpeechesController(ApplicationDbContext context, ApplicationUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Speeches
        public async Task<IActionResult> Index()
        {
            var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            if (user.MemberID != null)
            {
                return View(await _context.Speeches.Where(s=>s.MemberID == user.MemberID).ToListAsync());
            }
            return View();
        }

        // GET: Speeches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speech = await _context.Speeches
                .SingleOrDefaultAsync(m => m.SpeechID == id);
            if (speech == null)
            {
                return NotFound();
            }

            return View(speech);
        }

        // GET: Speeches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speeches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeechID,Title,Project,TimeConstraints")] Speech speech)
        {
            if (ModelState.IsValid)
            {
                var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
                if (user.MemberID != null)
                {
                    speech.MemberID = (Int32)user.MemberID;
                }
                
                _context.Add(speech);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(speech);
        }

        // GET: Speeches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speech = await _context.Speeches.SingleOrDefaultAsync(m => m.SpeechID == id);
            if (speech == null)
            {
                return NotFound();
            }
            return View(speech);
        }

        // POST: Speeches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeechID,Title,Project,TimeConstraints")] Speech speech)
        {
            if (id != speech.SpeechID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
                if (user.MemberID != null)
                {
                    speech.MemberID = (Int32)user.MemberID;
                }

                try
                {
                    _context.Update(speech);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeechExists(speech.SpeechID))
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
            return View(speech);
        }

        // GET: Speeches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speech = await _context.Speeches
                .SingleOrDefaultAsync(m => m.SpeechID == id);
            if (speech == null)
            {
                return NotFound();
            }

            return View(speech);
        }

        // POST: Speeches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speech = await _context.Speeches.SingleOrDefaultAsync(m => m.SpeechID == id);
            _context.Speeches.Remove(speech);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SpeechExists(int id)
        {
            return _context.Speeches.Any(e => e.SpeechID == id);
        }
    }
}
