using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using loginReg.Models;

namespace loginReg.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("FormSubmit")]
        public IActionResult FormSubmit(User userModel)
        {
            if(ModelState.IsValid)
            {
                return View("Success");
            }
            return View("Index");
        }
    }
}
