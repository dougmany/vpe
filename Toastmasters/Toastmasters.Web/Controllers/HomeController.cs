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
                    if (meeting.Toastmaster.MemberID == user.MemberID)
                    {
                        meeting.Toastmaster = null;
                    }

                    if (meeting.TableTopics.MemberID == user.MemberID)
                    {
                        meeting.TableTopics = null;
                    }

                    if (meeting.SpeakerI.MemberID == user.MemberID)
                    {
                        meeting.SpeakerI = null;
                    }

                    if (meeting.SpeakerII.MemberID == user.MemberID)
                    {
                        meeting.SpeakerII = null;
                    }

                    if (meeting.GeneralEvaluator.MemberID == user.MemberID)
                    {
                        meeting.GeneralEvaluator = null;
                    }

                    if (meeting.EvaluatorI.MemberID == user.MemberID)
                    {
                        meeting.EvaluatorII = null;
                    }

                    if (meeting.Inspirational.MemberID == user.MemberID)
                    {
                        meeting.Inspirational = null;
                    }

                    if (meeting.Joke.MemberID == user.MemberID)
                    {
                        meeting.Joke = null;
                    }

                    if (meeting.Timer.MemberID == user.MemberID)
                    {
                        meeting.Timer = null;
                    }

                    if (meeting.Grammarian.MemberID == user.MemberID)
                    {
                        meeting.Grammarian = null;
                    }

                    if (meeting.BallotCounter.MemberID == user.MemberID)
                    {
                        meeting.BallotCounter = null;
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
