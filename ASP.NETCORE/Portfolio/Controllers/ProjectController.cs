using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    public class ProjectController : Controller
    {
        [HttpGet]
        [Route("project")]
        public IActionResult Index()
        {
            return View("index");
        }


    }
}