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
            var blankMember = new Member { MemberID = 0, FirstName = "-Select", LastName = "Member -" };
            var members = _context.Members.OrderBy(m => m.FullName).Prepend(blankMember).ToArray();
            var membersList = new SelectList(members, "MemberID", "FullName");

            var model = new MeetingActionModel
            {
                Members = membersList
            };

            var toastmasterMeetingHistory = new MemberHistories();
            var tabletopicsMeetingHistory = new MemberHistories();
            var generalEvaluatorMeetingHistory = new MemberHistories();
            var evaluatorMeetingHistory = new MemberHistories();
            var speakerMeetingHistory = new MemberHistories();
            var timerMeetingHistory = new MemberHistories();
            var grammarianMeetingHistory = new MemberHistories();
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
                var generalEvaluatorMeeting = _context.Meetings
                    .Where(m => m.GeneralEvaluator.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                generalEvaluatorMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = generalEvaluatorMeeting == null ? new DateTime() : generalEvaluatorMeeting.MeetingDate
                });
                var evaluatorMeeting = _context.Meetings
                    .Where(m => m.EvaluatorI.MemberID == item.MemberID || m.EvaluatorII.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                evaluatorMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = evaluatorMeeting == null ? new DateTime() : evaluatorMeeting.MeetingDate
                });

                var speakerMeeting = _context.Meetings
                    .Where(m => m.SpeakerI.MemberID == item.MemberID || m.SpeakerII.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                speakerMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = speakerMeeting == null ? new DateTime() : speakerMeeting.MeetingDate
                });
                var timerMeeting = _context.Meetings
                    .Where(m => m.Timer.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                timerMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = timerMeeting == null ? new DateTime() : timerMeeting.MeetingDate
                });
                var grammarianMeeting = _context.Meetings
                    .Where(m => m.Grammarian.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                grammarianMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = grammarianMeeting == null ? new DateTime() : grammarianMeeting.MeetingDate
                });
                var inspirationalMeeting = _context.Meetings
                    .Where(m => m.Inspirational.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                inspirationalMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = inspirationalMeeting == null ? new DateTime() : inspirationalMeeting.MeetingDate
                });
                var jokeMeeting = _context.Meetings
                    .Where(m => m.Joke.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                jokeMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = jokeMeeting == null ? new DateTime() : jokeMeeting.MeetingDate
                });
                var ballotCounterMeeting = _context.Meetings
                    .Where(m => m.BallotCounter.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                ballotCounterMeetingHistory.Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = ballotCounterMeeting == null ? new DateTime() : ballotCounterMeeting.MeetingDate
                });
            }

            ViewBag.ToastmasterMemberHistory = toastmasterMeetingHistory.HtmlList;
            ViewBag.TabletopicsMeetingHistory = tabletopicsMeetingHistory.HtmlList;
            ViewBag.GeneralEvaluatorMeetingHistory = generalEvaluatorMeetingHistory.HtmlList;
            ViewBag.EvaluatorMeetingHistory = evaluatorMeetingHistory.HtmlList;
            ViewBag.SpeakerMeetingHistory = speakerMeetingHistory.HtmlList;
            ViewBag.TimerMeetingHistory = timerMeetingHistory.HtmlList;
            ViewBag.GrammarianMeetingHistory = grammarianMeetingHistory.HtmlList;
            ViewBag.InspirationalMeetingHistory = inspirationalMeetingHistory.HtmlList;
            ViewBag.JokeMeetingHistory = jokeMeetingHistory.HtmlList;
            ViewBag.BallotCounterMeetingHistory = ballotCounterMeetingHistory.HtmlList;

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
        public AgendaViewModel Next()
        {
            Meeting meeting = GetMeetingAfterDate(DateTime.Now);

            var nextMeeting = GetMeetingAfterDate(meeting.MeetingDate);

            if (meeting == null || nextMeeting == null)
            {
                return new AgendaViewModel(new Meeting(), new Meeting());
            }

            var model = new AgendaViewModel(meeting, nextMeeting);

            return model;
        }

        // GET: Meeting/Next
        public MeetingViewModel[] NextFive()
        {
            List<Meeting> meetingList = new List<Meeting>();

            FillSomeMeetings(DateTime.Now, meetingList, 5);

            var model = meetingList.Select(m => new MeetingViewModel(m)).ToArray();

            return model;
        }

        private void FillSomeMeetings(DateTime date, List<Meeting> list, Int32 number)
        {
            Meeting meeting = GetMeetingAfterDate(date);

            if (meeting != null)
            {
                list.Add(meeting);
                if (number > 0 )
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
