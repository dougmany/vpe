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

                model.Add( new MeetingViewModel(item)
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
                    GrammarianClass = dups.Contains(item.Grammarian.MemberID) ? "duplicate" : ""
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

        [AllowAnonymous]
        public ActionResult GetAgenda(Int32? meetingID)
        {
            Meeting meeting;

            if (meetingID == null)
            {
                meeting = _meetingHelpers.GetMeetingAfterDate(DateTime.Now);
            }
            else
            {
                meeting = _context.Meetings.FirstOrDefault(m => m.MeetingID == meetingID);
            }

            if (meeting == null)
            {
                return NotFound();
            }

            var nextMeeting = _meetingHelpers.GetMeetingAfterDate(meeting.MeetingDate);

            if (nextMeeting == null)
            {
                return NotFound();
            }

            var model = new AgendaViewModel(meeting, nextMeeting);

            Commands.LoadAgenda(model);

            var stream =  Commands.GetFile(Commands.FilesToGet.Agenda);
            return File(stream, "application/rtf", $"Agenda.rtf");
        }

        public ActionResult GetEmail()
        {
            var meetingList = new List<Meeting>();
            _meetingHelpers.FillSomeMeetings(DateTime.Now, meetingList, 5);
            var models = meetingList.Select(m => new MeetingViewModel(m)).ToArray();

            Commands.LoadEmail(models);

            var stream = Commands.GetFile(Commands.FilesToGet.Email);
            return File(stream, "application/rtf", $"Email.rtf");
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.MeetingID == id);
        }
    }
}
