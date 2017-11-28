using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toastmasters.Web.Models;
using Toastmasters.Web.Data;
using Toastmasters.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Toastmasters.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MeetingHelpers _meetingHelpers;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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

            return View(model);

        }

        public IActionResult RemoveMe(Int32 ID)
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
                    .Where(m => m.MeetingID == ID)
                    .FirstOrDefault();

                if (meeting != null)
                {
                    if (meeting.ToastmasterMemberID == user.MemberID)
                    {
                        meeting.ToastmasterMemberID = null;
                    }

                    if (meeting.TableTopicsMemberID == user.MemberID)
                    {
                        meeting.TableTopicsMemberID = null;
                    }

                    if (meeting.SpeakerIMemberID == user.MemberID)
                    {
                        meeting.SpeakerIMemberID = null;
                    }

                    if (meeting.SpeakerIIMemberID == user.MemberID)
                    {
                        meeting.SpeakerIIMemberID = null;
                    }

                    if (meeting.GeneralEvaluatorMemberID == user.MemberID)
                    {
                        meeting.GeneralEvaluatorMemberID = null;
                    }

                    if (meeting.EvaluatorIMemberID == user.MemberID)
                    {
                        meeting.EvaluatorIIMemberID = null;
                    }

                    if (meeting.InspirationalMemberID == user.MemberID)
                    {
                        meeting.InspirationalMemberID = null;
                    }

                    if (meeting.JokeMemberID == user.MemberID)
                    {
                        meeting.JokeMemberID = null;
                    }

                    if (meeting.TimerMemberID == user.MemberID)
                    {
                        meeting.TimerMemberID = null;
                    }

                    if (meeting.GrammarianMemberID == user.MemberID)
                    {
                        meeting.GrammarianMemberID = null;
                    }

                    if (meeting.BallotCounterMemberID == user.MemberID)
                    {
                        meeting.BallotCounterMemberID = null;
                    }

                    _context.SaveChanges();

                }
            }
            List<Meeting> meetingList = new List<Meeting>();

            _meetingHelpers.FillSomeMeetings(DateTime.Now, meetingList, 5);

            var model = meetingList.Select(m => new MeetingViewModel(m)).ToArray();

            return View("Index", model);

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
