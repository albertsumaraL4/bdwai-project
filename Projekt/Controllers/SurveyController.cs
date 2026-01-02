using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class SurveyController : BaseController
    {

        public SurveyController(ProjektContext context) : base(context)
        {
        }

        [HttpGet]
        public IActionResult SurveyCreator()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult CreateSurveyTitle()
        //{
        //    return View("SurveyCreator");
        //}

        [HttpPost]
        public IActionResult CreateSurveyTitle(Survey model)
        {
            _context.Surveys.Add(model);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("SurveyId", model.Id);
            TempData["SurveyId"] = model.Id;

            return RedirectToAction("CreateSurveyQuestion");

        }

        [HttpGet]
        public IActionResult CreateSurveyQuestion()
        {
            return View("SurveyQuestionCreator");
        }

        [HttpPost]
        public IActionResult CreateSurveyQuestion(Survey model)
        {

            model.Id = (int) HttpContext.Session.GetInt32("SurveyId");
            ViewData["Title"] = model.Title;

            _context.Surveys.Add(model);
            _context.SaveChanges();

            return View("SurveyQuestionCreator");



        }
    }
}
