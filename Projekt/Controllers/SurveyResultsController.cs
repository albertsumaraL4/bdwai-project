using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;

using Projekt.Models;


namespace Projekt.Controllers
{
    public class SurveyResultsController : BaseController
    {

        public SurveyResultsController(UserManager<ApplicationUser> userManager, ProjektContext context) : base(userManager, context)
        {
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
        public IActionResult GetResults(int surveyId)
        {


            string? userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return BadRequest();
            }

            var surveyResult = _context.SurveyResults
                .Where(s => s.SurveyId == surveyId && s.UserId == userId)
                .OrderBy(a => a.Id)
                .Include(s => s.ChoosenAnswers)
                .FirstOrDefault();

            if (surveyResult == null)
            {
                return NotFound();
            }

            return Json(surveyResult);
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

    }
}
