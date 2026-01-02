using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;


namespace Projekt.Controllers
{


    public class HomeController : Controller
    {

        protected readonly ProjektContext _context;

        public HomeController(ProjektContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
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

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }      
    }
}
