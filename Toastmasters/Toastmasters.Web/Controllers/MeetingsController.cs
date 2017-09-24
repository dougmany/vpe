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
            var blankMember = new Member { MemberID = 0, FirstName = "-Select", LastName = "Member -" };
            var members = _context.Members.OrderBy(m => m.FullName).Prepend(blankMember).ToArray();
            var membersList = new SelectList(members, "MemberID", "FullName");

            var model = new MeetingActionModel
            {
                Members = membersList
            };

            var toastmasterMeetingHistory = new MemberHistories();
            var tabletopicsMeetingHistory = new MemberHistories();
            var GeneralEvaluatorMeetingHistory = new MemberHistories();
            var evaluatorIMeetingHistory = new MemberHistories();
            var evaluatorIItoastmasterMeetingHistory =new MemberHistories();
            var speakerIMeetingHistory = new MemberHistories();
            var speakerIIMeetingHistory =new MemberHistories();
            var timerMeetingHistory = new MemberHistories();
            var grammarianMeetingHistory = new List<MemberHistory>();
            var inspirationalMeetingHistory = new MemberHistories();
            var jokeMeetingHistory = new MemberHistories();
            var ballotCounterMeetingHistory = new MemberHistories();

            foreach (var item in _context.Members.ToArray())
            {
                var toastmasterMeeting = _context.Meetings
                    .Where(m => m.Toastmaster.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                toastmasterMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = toastmasterMeeting == null ? new DateTime() : toastmasterMeeting.MeetingDate
                });
                var tabletopicsMeeting = _context.Meetings
                    .Where(m => m.TableTopics.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                tabletopicsMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = tabletopicsMeeting == null ? new DateTime() : tabletopicsMeeting.MeetingDate
                });
            }

            ViewBag.ToastmasterMemberHistory = toastmasterMeetingHistory.HtmlList;
            ViewBag.TabletopicsMeetingHistory = tabletopicsMeetingHistory.HtmlList;
            ViewBag.MemberHistory = toastmasterMeetingHistory.HtmlList;

            return View(model);
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
            var members = _context.Members.OrderBy(m => m.FullName);
            var builder = new MeetingActionBuilder(members.ToArray());
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

        // GET: Meeting/Next
        public MeetingViewModel Next()
        {
            var meeting = _context.Meetings
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
                .Where(m => m.MeetingDate > DateTime.Now).OrderBy(m => m.MeetingDate).FirstOrDefault();

            var nextMeeting = _context.Meetings
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
                .Where(m => m.MeetingDate > meeting.MeetingDate).OrderBy(m => m.MeetingDate).FirstOrDefault();
            if (meeting == null || nextMeeting == null)
            {
                return new MeetingViewModel(new Meeting(), new Meeting());
            }

            var model = new MeetingViewModel(meeting, nextMeeting);

            return model;
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.MeetingID == id);
        }
    }
}
