using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toastmasters.Web.Data;
using Toastmasters.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Toastmasters.Web.Helpers;

namespace Toastmasters.Web.Controllers
{
    [Authorize]
    public class MeetingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Meeting
        public ActionResult Index()
        {
            var meetings = _context.Meetings
                .Include(m => m.Toastmaster)
                .Include(m => m.TableTopics)
                .Include(m => m.SpeakerI)
                .Include(m => m.SpeakerII)
                .Include(m => m.GeneralEvaluator)
                .Include(m => m.EvaluatorI)
                .Include(m => m.EvaluatorII)
                .Include(m => m.Inspirational)
                .Include(m => m.Joke)
                .Include(m => m.Timer)
                .Include(m => m.Grammarian)
                .Include(m => m.BallotCounter)
                .OrderByDescending(m => m.MeetingDate)
                .ToList();

            return View(meetings);
        }

        // GET: Meeting/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings.SingleOrDefaultAsync(m => m.MeetingID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meeting/Create
        public IActionResult Create()
        {
            var builder = new MeetingActionBuilder(_context.Members.ToArray(), _context.Meetings.ToArray());

            return View(builder.View());
        }

        // POST: Meeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MeetingActionModel meeting)
        {
            if (ModelState.IsValid)
            {
                var builder = new MeetingActionBuilder(_context.Members.ToArray());
                String changes;
                _context.Meetings.Add(builder.Create(meeting, out changes));
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(meeting);
        }

        // GET: Meeting/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = _context.Meetings.SingleOrDefault(m => m.MeetingID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            var builder = new MeetingActionBuilder(_context.Members.ToArray(), _context.Meetings.ToArray());
            var model = builder.View(meeting);

            return View(model);
        }

        // POST: Meeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MeetingActionModel meeting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var builder = new MeetingActionBuilder(_context.Members.ToArray());
                    List<String> changes;
                    var entity = _context.Meetings.Single(m => m.MeetingID == meeting.MeetingID);
                    builder.Update(meeting, entity, out changes);

                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.MeetingID))
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
            return View(meeting);
        }

        // GET: Meeting/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = _context.Meetings
                .SingleOrDefault(m => m.MeetingID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var meeting = _context.Meetings.SingleOrDefault(m => m.MeetingID == id);
            _context.Meetings.Remove(meeting);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GenerateFiles()
        {
            var meeting = GetMeetingAfterDate(DateTime.Now);
            var nextMeeting = GetMeetingAfterDate(meeting.MeetingDate);
            var model = new AgendaViewModel(meeting, nextMeeting);

            Commands.LoadAgenda(model);

            var meetingList = new List<Meeting>();
            FillSomeMeetings(DateTime.Now, meetingList, 5);
            var models = meetingList.Select(m => new MeetingViewModel(m)).ToArray();

            Commands.LoadEmail(models);

            return RedirectToAction("Index");
        }

        public ActionResult GetAgenda()
        {
            var stream =  Commands.GetFile(Commands.FilesToGet.Agenda);
            return File(stream, "application/rtf", $"Agenda.rtf");
        }

        public ActionResult GetEmail()
        {
            var stream = Commands.GetFile(Commands.FilesToGet.Email);
            return File(stream, "application/rtf", $"Email.rtf");
        }

        private void FillSomeMeetings(DateTime date, List<Meeting> list, Int32 number)
        {
            Meeting meeting = GetMeetingAfterDate(date);

            if (meeting != null)
            {
                list.Add(meeting);
                if (number > 0)
                {
                    FillSomeMeetings(meeting.MeetingDate, list, --number);
                }
                return;
            }
            return;
        }

        private Meeting GetMeetingAfterDate(DateTime beforeDate)
        {
            return _context.Meetings
                .Include(m => m.Toastmaster)
                .Include(m => m.TableTopics)
                .Include(m => m.SpeakerI)
                .Include(m => m.SpeakerII)
                .Include(m => m.GeneralEvaluator)
                .Include(m => m.EvaluatorI)
                .Include(m => m.EvaluatorII)
                .Include(m => m.Inspirational)
                .Include(m => m.Joke)
                .Include(m => m.Timer)
                .Include(m => m.Grammarian)
                .Include(m => m.BallotCounter)
                .Include(m => m.President)
                .Include(m => m.Sargent)
                .Where(m => m.MeetingDate > beforeDate)
                .OrderBy(m => m.MeetingDate)
                .FirstOrDefault();
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.MeetingID == id);
        }
    }
}
