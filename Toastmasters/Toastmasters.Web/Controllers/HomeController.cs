using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Toastmasters.Web.Models;
using Toastmasters.Web.Data;
using Toastmasters.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Toastmasters.Web.Helpers.MeetingHelpers;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Toastmasters.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MeetingHelpers _meetingHelpers;
        private readonly ApplicationUserManager _userManager;

        public HomeController(ApplicationDbContext context, ApplicationUserManager userManager)
        {
            _context = context;
            _meetingHelpers = new MeetingHelpers(context);
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Meeting> meetingList = new List<Meeting>();

            _meetingHelpers.FillSomeMeetings(DateTime.Now, meetingList, 5);

            var model = meetingList.Select(m => new MeetingViewModel(m)).ToArray();

            var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));

            var speeches = _context.Speeches.Where(s => s.MemberID ==user.MemberID).OrderBy(m => m.Title).ToArray();
            var speechList = new SelectList(speeches, "SpeechID", "Title");
            ViewBag.Speeches = speechList.Prepend(new SelectListItem { Text = "-Add New-", Value = "0" });

            return View(model);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult RemoveMe(Int32 meetingID)
        {
            var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));

            if (user != null && user.MemberID != null)
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
                    .Where(m => m.MeetingID == meetingID)
                    .FirstOrDefault();

                var Absences = new List<Absence>();
                if (meeting != null)
                {
                    if (meeting.ToastmasterMemberID == user.MemberID)
                    {
                        meeting.ToastmasterMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Toastmaster
                        });
                    }

                    if (meeting.TableTopicsMemberID == user.MemberID)
                    {
                        meeting.TableTopicsMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Tabletopics
                        });
                    }

                    if (meeting.SpeakerIMemberID == user.MemberID)
                    {
                        meeting.SpeakerIMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Speaker
                        });
                    }

                    if (meeting.SpeakerIIMemberID == user.MemberID)
                    {
                        meeting.SpeakerIIMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Speaker
                        });
                    }

                    if (meeting.GeneralEvaluatorMemberID == user.MemberID)
                    {
                        meeting.GeneralEvaluatorMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.GeneralEvaluator
                        });
                    }

                    if (meeting.EvaluatorIMemberID == user.MemberID)
                    {
                        meeting.EvaluatorIMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Evaluator
                        });
                    }

                    if (meeting.EvaluatorIIMemberID == user.MemberID)
                    {
                        meeting.EvaluatorIIMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Evaluator
                        });
                    }

                    if (meeting.InspirationalMemberID == user.MemberID)
                    {
                        meeting.InspirationalMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Inspirational
                        });
                    }

                    if (meeting.JokeMemberID == user.MemberID)
                    {
                        meeting.JokeMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Joke
                        });
                    }

                    if (meeting.TimerMemberID == user.MemberID)
                    {
                        meeting.TimerMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Timer
                        });
                    }

                    if (meeting.GrammarianMemberID == user.MemberID)
                    {
                        meeting.GrammarianMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.Grammarian
                        });
                    }

                    if (meeting.BallotCounterMemberID == user.MemberID)
                    {
                        meeting.BallotCounterMemberID = null;
                        Absences.Add(new Absence
                        {
                            MemberID = (Int32)user.MemberID,
                            MeetingID = meeting.MeetingID,
                            Role = MeetingRoleNames.BallotCounter
                        });
                    }

                    if (Absences.Count > 0)
                    {
                        _context.Absences.AddRange(Absences.ToArray());
                    }

                    _context.SaveChanges();

                }
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddMe(Int32 meetingID, MeetingRole role)
        {
            var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));

            if (user != null && user.MemberID != null)
            {
                var meeting = _context.Meetings
                    .Where(m => m.MeetingID == meetingID)
                    .FirstOrDefault();

                if (meeting != null)
                {
                    meeting.GetType().GetProperty(role.ToString() + "MemberID").SetValue(meeting, user.MemberID);
                    _context.SaveChanges();
                }

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SetTheme(Int32 meetingID, String meetingTheme)
        {
            var meeting = _context.Meetings.Where(m => m.MeetingID == meetingID).FirstOrDefault();
            var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));

            if (user.MemberID != meeting.ToastmasterMemberID)
            {
                return NotFound();
            }
            if (meeting != null)
            {
                meeting.MeetingTheme = meetingTheme;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SetSpeech(Int32 meetingID, Int32 speechID, Boolean isINotII)
        {
            if (speechID == 0)
            {
                return RedirectToAction("Create", "Speeches");
            }

            var meeting = _context.Meetings.Where(m => m.MeetingID == meetingID).FirstOrDefault();
            var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));

            if (meeting == null)
            {
                return NotFound();
            }

            if (isINotII)
            {
                if (user.MemberID != meeting.SpeakerIMemberID)
                {
                    return NotFound();
                }

                meeting.SpeechISpeechID = speechID;
                _context.SaveChanges();
            }
            else
            {
                if (user.MemberID != meeting.SpeakerIIMemberID)
                {
                    return NotFound();
                }

                meeting.SpeechIISpeechID = speechID;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
