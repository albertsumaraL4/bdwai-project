using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class RegistrationController : BaseController
    {

        public RegistrationController(ProjektContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {

            var sessionResult = CheckSession();
            if (sessionResult != null)
            {
                return sessionResult;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Registration(User user)
        {

            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                ViewBag.info = $"Rejestracja zakończona pomyślnie. Witaj, {user.Name}\n";
                return View();
            }

            else return View();

        }

    }
}
