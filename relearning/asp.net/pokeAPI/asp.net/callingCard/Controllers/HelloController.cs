using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace callingCard.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet]
        [Route("{FirstName}/{LastName}/{Age}/{favoriteColor}")]
        public JsonResult Index(string FirstName, string LastName, int Age, string favoriteColor)
        {
            var anonObject = new
            {
                FirstName = FirstName,
                LastName = LastName,
                age = Age,
                favoriteColor = favoriteColor
            };
            return Json(anonObject);
        } 
    }
}
