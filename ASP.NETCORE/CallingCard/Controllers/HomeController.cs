using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet]
        [Route("index/{fName}/{lName}/{Age}/{Color}")]
        public JsonResult Index(string fName, string lName, int Age, string Color)
        {
            var AnonObject = new 
            {
                firstName = fName,
                lastName = lName,
                age = Age,
                color = Color,
            };
            return Json(AnonObject);
        }
    }
}
