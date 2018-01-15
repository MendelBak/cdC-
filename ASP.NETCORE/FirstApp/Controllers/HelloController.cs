using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    public class HelloController : Controller
    {

        // A GET method
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        // Other code
        [HttpGet]
        [Route("displayint")]
        public JsonResult DisplayInt()
        {
            var AnonObject = new
            {
                FirstName = "Raz",
                LastName = "Aquato",
                Age = 10
            };
            return Json(AnonObject);
        }


        // A POST method
        [HttpPost]
        [Route("")]
        public IActionResult Other()
        {
            // Return a view (We'll learn how soon!)
            return null;
        }

    }
}
