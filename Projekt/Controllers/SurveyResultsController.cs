using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using System.Linq;
using Projekt.Models;


namespace Projekt.Controllers
{
    public class SurveyResultsController : BaseController
    {

        private int[] completedSurveys;

        public SurveyResultsController(UserManager<ApplicationUser> userManager, ProjektContext context) : base(userManager, context)
        {

            completedSurveys = CompletedSurveys().ToArray();

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SurveyStatsChooser()
        {

            return View();

        }


        [HttpGet]
        public IActionResult ListSurveysCompleted()
        {

            var completedSurveys = _context.SurveyResults
                .Select(sr => sr.SurveyId)
                .Distinct()
                .ToList();

            var surveys = _context.Surveys
                .Where(s => completedSurveys.Contains(s.Id))
                .Select(s => new { s.Id, s.Title })
                .ToList();

            return Json(surveys);

        }

        [HttpGet]
        public IActionResult ListSurveys()
        {

            var surveys = _context.Surveys
                .Select(s => new { s.Id, s.Title })
                .ToList();
            return Json(surveys);

        }

        [HttpPost]
        public IActionResult Results([FromBody] SurveyResults surveyResults)
        {


            string? UserId = _userManager.GetUserId(User);

            if (UserId == null)
            {
                return BadRequest();
            }

            surveyResults.UserId = UserId;

            //Console.WriteLine("obiekt: "surveyResults);
            _context.SurveyResults.Add(surveyResults);
            _context.SaveChanges();

            return Ok(new { success = true });
        }

        [HttpGet]
        public IActionResult GetResults(int? surveyId)
        {
            var surveyResults = _context.SurveyResults
                .Where(s => s.SurveyId == surveyId)
                .OrderBy(a => a.Id)
                .Include(s => s.ChoosenAnswers)
                .ToList();

            if (surveyResults.Count == 0)
                return NotFound();

            return Json(surveyResults);
        }


        [HttpGet]
        public IActionResult SurveyStats()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetStats(int surveyId)
        {
           
            if (surveyId == 0)
            {
                return BadRequest();
            }

            var surveyResults = _context.SurveyResults
                .Where(sr => sr.SurveyId == surveyId)
                .SelectMany(sr => sr.ChoosenAnswers)
                .ToList();

            var stats = surveyResults
                .GroupBy(ca => new { ca.QuestionId, ca.AnswerId })
                .Select(g => new { g.Key.QuestionId, g.Key.AnswerId, Count = g.Count() })
                .ToList()
                .GroupBy(x => x.QuestionId)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToDictionary(x => x.AnswerId, x => x.Count)
                );

            return Json(stats);

        }

        [HttpGet]
        public IActionResult IsCompleted(int surveyId)
        {
            Console.WriteLine("id: " + surveyId);
            return Ok(completedSurveys.Contains(surveyId));

        }

        public List<int> CompletedSurveys()
        {

            var completedSurveys = _context.SurveyResults
                .Select(sr => sr.SurveyId)
                .Distinct()
                .ToList();

            Console.WriteLine("test: " + string.Join(", ", completedSurveys));

            return completedSurveys;

        }

        [HttpGet]
        public IActionResult GetCities()
        {

            var cities = Enum.GetValues(typeof(ApplicationUser.Miasta))
                .Cast<ApplicationUser.Miasta>()
                .Select(c => new
                {
                    Id = (int)c,
                    Name = c.ToString()
                });

            return Json(cities);

        }

        [HttpGet] 
        public IActionResult SurveyStatsFilter()
        {

            return View();

        }

    }
}
