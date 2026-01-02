using Microsoft.AspNetCore.Mvc;
using Projekt.Data;

namespace Projekt.Controllers
{
    public class BaseController : Controller
    {

        protected readonly ProjektContext _context;

        public BaseController(ProjektContext context)
        {
            _context = context;
        }
        protected IActionResult CheckSession()
        {
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                ViewData["Username"] = HttpContext.Session.GetString("Username");
                return RedirectToAction("Logged", "Login");
            }

            return null;
        }
    }
}
