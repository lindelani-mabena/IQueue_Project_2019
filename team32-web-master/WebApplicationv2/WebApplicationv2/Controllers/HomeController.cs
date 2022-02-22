using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationv2.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.Message = TempData["SuccessMessage"].ToString();
                return View();
            }

            if (User.IsInRole("client"))
            {
                ViewBag.UpdateProfile = true;

                if (HttpContext.Session.GetString("FirstName") != null ||
                    HttpContext.Session.GetString("LastName") != null ||
                    HttpContext.Session.GetString("Title") != null)
                {
                    var firstName = HttpContext.Session.GetString("FirstName");
                    var lastName = HttpContext.Session.GetString("LastName");
                    var title = HttpContext.Session.GetString("Title");

                    ViewBag.Greeting = $"{title} {lastName}";
                    ViewBag.UpdateProfile = false;
                }
            }
            else if (User.IsInRole("consultant"))
            {
                ViewBag.Greeting = "Consultant";
            } else if (User.IsInRole("manager"))
            {
                ViewBag.Greeting = "Manager";
            } else if (User.IsInRole("admin"))
            {
                ViewBag.Greeting = "Admin";
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
