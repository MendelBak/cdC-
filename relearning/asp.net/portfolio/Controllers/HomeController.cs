using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace portfolio.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Projects")]
        public IActionResult Projects()
        {

            return View();
        }

        [HttpGet]
        [Route("Contact")]
        public IActionResult Contact()
        {
            System.Console.WriteLine();
            return View();
        }

        [HttpPost]
        [Route("contactSubmit")]
        public IActionResult contactSubmit(string name, string email, string message)
        {
            System.Console.WriteLine(name + message + email);
            return Redirect("Contact");
        }

    }
}
