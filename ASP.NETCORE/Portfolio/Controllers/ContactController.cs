using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        [Route("contact")]
        public IActionResult Index()
        {
            return View("index");
        }


    }
}