using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoLeague.Models;
using DojoLeague.Factory;

namespace DojoLeague.Controllers
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
        [Route("NewNinja")]
        public IActionResult NewNinja() 
        {
            return RedirectToAction("index");
        }
    }
}
