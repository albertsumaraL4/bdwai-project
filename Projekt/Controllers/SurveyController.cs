using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;
using System.Text.Json;

namespace Projekt.Controllers
{
    public class SurveyController : BaseController
    {

        public SurveyController(UserManager<ApplicationUser> userManager, ProjektContext context) : base(userManager, context)
        {
        }

        [HttpGet]
        public IActionResult SurveyCreator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Results([FromBody] JsonElement surveyJson)
        {
            var title = surveyJson.GetProperty("title").GetString();

            if (string.IsNullOrEmpty(title))
                return BadRequest("Title is required");

            var survey = new Survey { Title = title! };

            foreach (var questionJson in surveyJson.GetProperty("questions").EnumerateArray())
            {
                var question = new Question
                {
                    Content = questionJson.GetProperty("content").GetString()!
                };

                foreach (var answerJson in questionJson.GetProperty("answers").EnumerateArray())
                {
                    var answer = new Answer
                    {
                        Content = answerJson.GetString()!
                    };
                    question.Answers.Add(answer);
                }

                survey.Questions.Add(question);
            }

            _context.Surveys.Add(survey);
            _context.SaveChanges();

            return Ok(new { success = true });
        }


        //[HttpGet]
        //public IActionResult CreateSurveyTitle()
        //{
        //    return View("SurveyCreator");
        //}

        //[HttpPost]
        //public IActionResult CreateSurveyTitle(Survey model)
        //{
        //    _context.Surveys.Add(model);
        //    _context.SaveChanges();

        //    HttpContext.Session.SetInt32("SurveyId", model.Id);
        //    TempData["SurveyId"] = model.Id;

        //    return RedirectToAction("CreateSurveyQuestion");

        //}

        //[HttpGet]
        //public IActionResult CreateSurveyQuestion()
        //{
        //    return View("SurveyQuestionCreator");
        //}

        //[HttpPost]
        //public IActionResult CreateSurveyQuestion(String content)
        //{

        //    var question = new Question
        //    {
        //        Content = content,
        //        SurveyId = (int)HttpContext.Session.GetInt32("SurveyId")

        //    };



        //    _context.Questions.Add(question);
        //    _context.SaveChanges();

        //    HttpContext.Session.SetInt32("QuestionId", question.Id);

        //    return View("SurveyAnswerCreator");

        //}

        //[HttpGet]
        //public IActionResult CreateSurveyAnswer()
        //{
        //    return View("SurveyAnswerCreator");
        //}

        //[HttpPost]
        //public IActionResult CreateSurveyAnswer(String content)
        //{

        //    var answer = new Answer
        //    {
        //        Content = content,
        //        QuestionId = (int)HttpContext.Session.GetInt32("QuestionId")

        //    };


        //    _context.Answers.Add(answer);
        //    _context.SaveChanges();

        //    return View("SurveyAnswerCreator");

        //}

        [HttpGet]
        public IActionResult SurveyChooser()
        {
            return View();

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
        public IActionResult SaveChoosenSurvey([FromBody] int surveyId)
        {

            HttpContext.Session.SetInt32("SurveyId", surveyId);

            Console.WriteLine("surveyId" + ": " +surveyId);

            return Ok(new { success = true });
        }

        [HttpGet]
        public IActionResult GetChoosenSurvey()
        {
            int? surveyId = HttpContext.Session.GetInt32("SurveyId");
            if (surveyId.HasValue)
                return Json(new { SurveyId = surveyId.Value });
            else
                return Json(new { SurveyId = (int?)null });
        }

        [HttpGet]
        public IActionResult SurveyCompleter()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListQuestions(int surveyId)
        {
            var questions = _context.Questions
                .Where(q => q.SurveyId == surveyId)
                .OrderBy(q => q.Id)
                .Select(q => new { q.Id, q.Content })
                .ToList();

            return Json(questions);
        }

        [HttpGet]
        public IActionResult ListAnswers(int questionId)
        {
            var answers = _context.Answers
                .Where(a => a.QuestionId == questionId)
                .OrderBy(a => a.Id)
                .Select(a => new { a.Id, a.Content })
                .ToList();

            return Json(answers);
        }

        [HttpGet]
        public IActionResult ListSurveysTitles()
        {
            var surveysTitles = _context.Surveys
                .Select(s => s.Title)
                .ToList();

            return Json(surveysTitles);

        }

        

    }
}
