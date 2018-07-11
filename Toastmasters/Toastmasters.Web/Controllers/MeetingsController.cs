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
    [Authorize(Roles = "Scheduler")]
    public class MeetingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MeetingHelpers _meetingHelpers;

        public MeetingsController(ApplicationDbContext context)
        {
            _context = context;
            _meetingHelpers = new MeetingHelpers(context);
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

            var model = new List<MeetingViewModel>();

            foreach (var item in meetings)
            {
                var meetingmembers = new Member[]
                {
                    item.Toastmaster,
                    item.TableTopics,
                    item.SpeakerI,
                    item.SpeakerII,
                    item.GeneralEvaluator,
                    item.EvaluatorI,
                    item.EvaluatorII,
                    item.Inspirational,
                    item.Joke,
                    item.Timer,
                    item.Grammarian,
                    item.BallotCounter
                };
                var dups = meetingmembers.GroupBy(d => d.MemberID).Where(d => d.Count() > 1).Select(d => d.Key).ToArray();

                model.Add(new MeetingViewModel(item)
                {
                    ToastmasterClass = dups.Contains(item.Toastmaster.MemberID) ? "duplicate" : "",
                    TableTopicsClass = dups.Contains(item.TableTopics.MemberID) ? "duplicate" : "",
                    SpeakerIClass = dups.Contains(item.SpeakerI.MemberID) ? "duplicate" : "",
                    SpeakerIIClass = dups.Contains(item.SpeakerII.MemberID) ? "duplicate" : "",
                    GeneralEvaluatorClass = dups.Contains(item.GeneralEvaluator.MemberID) ? "duplicate" : "",
                    EvaluatorIClass = dups.Contains(item.EvaluatorI.MemberID) ? "duplicate" : "",
                    EvaluatorIIClass = dups.Contains(item.EvaluatorII.MemberID) ? "duplicate" : "",
                    InspirationalClass = dups.Contains(item.Inspirational.MemberID) ? "duplicate" : "",
                    JokeClass = dups.Contains(item.Joke.MemberID) ? "duplicate" : "",
                    TimerClass = dups.Contains(item.Timer.MemberID) ? "duplicate" : "",
                    GrammarianClass = dups.Contains(item.Grammarian.MemberID) ? "duplicate" : "",
                    BallotCounterClass = dups.Contains(item.BallotCounter.MemberID) ? "duplicate" : ""
                });
            }

            return View(model);
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
            var builder = new MeetingActionBuilder(_context.Members.Where(m => m.IsActive).OrderBy(m => m.FirstInitial).ToArray(), _context.Meetings.ToArray());
            var model = builder.View();
            model.MeetingDate = DateTime.Now;
            var sargent = _context.Members.Where(m => m.IsSargent).FirstOrDefault();
            if (sargent != null)
            {
                model.SargentMemberID = sargent.MemberID;
            }
            var president = _context.Members.Where(m => m.IsPresident).FirstOrDefault();
            if (president != null)
            {
                model.PresidentMemberID = president.MemberID;
            }

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
                var builder = new MeetingActionBuilder(_context.Members.Where(m => m.IsActive).OrderBy(m => m.FirstInitial).ToArray());
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

            var builder = new MeetingActionBuilder(_context.Members.Where(m => m.IsActive).OrderBy(m => m.FirstInitial).ToArray(), _context.Meetings.ToArray());
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
                    var builder = new MeetingActionBuilder(_context.Members.Where(m => m.IsActive).OrderBy(m => m.FirstInitial).ToArray());
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

        [AllowAnonymous]
        public ActionResult GetAgenda(Int32 id)
        {
            var meeting = _meetingHelpers.GetMeeting((Int32)id);

            if (meeting == null)
            {
                return NotFound();
            }

            var nextMeeting = _meetingHelpers.GetMeetingAfterDate(meeting.MeetingDate);

            if (nextMeeting == null)
            {
                return NotFound();
            }

            var model = new AgendaViewModel(meeting, nextMeeting, _context.Clubs.FirstOrDefault());

            Commands.LoadAgenda(model);
#if DEBUG
#else
            Commands.Latex2Rtf("Agenda");
#endif

            var stream = Commands.GetFile(Commands.FilesToGet.Agenda);
            return File(stream, "application/rtf", $"Agenda.rtf");
        }

        [AllowAnonymous]
        public ActionResult GetNextAgenda()
        {
            var meeting = _meetingHelpers.GetMeetingAfterDate(DateTime.Now.AddHours(-9));

            if (meeting == null)
            {
                return NotFound();
            }

            var nextMeeting = _meetingHelpers.GetMeetingAfterDate(meeting.MeetingDate);

            if (nextMeeting == null)
            {
                return NotFound();
            }

            var model = new AgendaViewModel(meeting, nextMeeting, _context.Clubs.FirstOrDefault());

            Commands.LoadAgenda(model);
#if DEBUG
#else
            Commands.Latex2Rtf("Agenda");
#endif

            var stream = Commands.GetFile(Commands.FilesToGet.Agenda);
            return File(stream, "application/rtf", $"Agenda.rtf");
        }

        public ActionResult GetEmail(Int32? id)
        {
            var meetingList = new List<Meeting>();
            DateTime beforeMeetingDate;

            if (id == null)
            {
                beforeMeetingDate = DateTime.Now.AddHours(-9);
            }
            else
            {
                var beforeMeeting = _context.Meetings.FirstOrDefault(m => m.MeetingID == id);
                beforeMeetingDate = beforeMeeting == null ? DateTime.Now.AddHours(-9) : beforeMeeting.MeetingDate;
            }

            _meetingHelpers.FillSomeMeetings(beforeMeetingDate, meetingList, 5);
            var models = meetingList.Select(m => new MeetingViewModel(m)).ToArray();

            var memberEmails = _context.Members.Where(m => m.IsActive).Select(m => m.Email).ToArray();
            var guestEmails = _context.Clubs.FirstOrDefault().GuestEmails;

            models[0].EmailTo = String.Join(";", memberEmails) + ";" + guestEmails;

            Commands.LoadEmail(models);
            #if DEBUG
#else
            Commands.Latex2Rtf("Email");
#endif

            var stream = Commands.GetFile(Commands.FilesToGet.Email);
            return File(stream, "application/rtf", $"Email.rtf");
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.MeetingID == id);
        }
    }
}
