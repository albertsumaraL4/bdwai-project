using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(ProjektContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var sessionResult = CheckSession();
            if (sessionResult != null)
            {
                return sessionResult;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUser model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user == null)
            {
                ModelState.AddModelError(nameof(model.Username), "Niepoprawne dane.");
                return View(model);
            }

            if (user.Password != model.Password)
            {
                ModelState.AddModelError(nameof(model.Password), "Niepoprawne dane.");
                return View(model);
            }


            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("UserId", user.Id);

            if (user.SuperUser == 1)
            {
                return RedirectToAction("SurveyCreator", "Survey");

            }

            return RedirectToAction("Logged", "Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return View("Login");
        }

        [HttpGet]
        public IActionResult Logged()
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            return View("Logged");
        }

    }
}
