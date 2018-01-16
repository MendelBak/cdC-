using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DojoSurvey.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("index");
        }

        [HttpPost]
        [Route("formSubmit")]
        public IActionResult results(string name, string location, string comment)
        {
            ViewBag.ResultName = name;
            ViewBag.ResultLocation = location;
            ViewBag.ResultComment = comment;

            return View("results");
        }
    }
}