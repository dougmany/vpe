using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toastmasters.Web.Models;
using Toastmasters.Web.Data;
using Toastmasters.Web.Helpers;

namespace Toastmasters.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MeetingHelpers _meetingHelpers;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            _meetingHelpers = new MeetingHelpers(context);
        }

        public IActionResult Index()
        {
            List<Meeting> meetingList = new List<Meeting>();

            _meetingHelpers.FillSomeMeetings(DateTime.Now, meetingList, 5);

            var model = meetingList.Select(m => new MeetingViewModel(m)).ToArray();

            return View(model);

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
