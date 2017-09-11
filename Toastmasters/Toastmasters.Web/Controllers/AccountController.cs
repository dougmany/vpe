using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Toastmasters.Web.Controllers
{
    public class AccountController : Controller
    {
        private AuthenticationManager Authentication
        {
            get
            {
                return HttpContext.Authentication;
            }
        }

        private void SignInOwin(string name, bool rememberMe, IEnumerable<string> roles)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, name) }, "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);

            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }


            //Authentication.SignIn(new AuthenticationProperties
            //{
            //    IsPersistent = rememberMe
            //}, identity);
        }

        public ActionResult SignIn()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult SignIn(SignInInput input)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        input.Password = null;
        //        input.Login = null;
        //        return View(input);
        //    }

        //    var user = userService.Get(input.Login, input.Password);

        //    if (user == null)
        //    {
        //        ModelState.AddModelError("", "incorrect username or password");
        //        return View();
        //    }

        //    SignInOwin(user.Login, input.Remember, user.Roles.Select(o => o.Name));


        //    return RedirectToAction("index", "home");
        //}

        public ActionResult SignOff()
        {
            //Authentication.SignOut();
            return RedirectToAction("SignIn", "Account");
        }
    }
}
