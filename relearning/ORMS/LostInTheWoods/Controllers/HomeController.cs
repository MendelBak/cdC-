using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LostInTheWoods.Models;
using LostInTheWoods.Factory;

namespace LostInTheWoods.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrailFactory trailFactory;
    
        public HomeController()
        {
            trailFactory = new TrailFactory();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.AllTrails = trailFactory.GetAllTrails();
            return View();
        }

        // View with form to add new trail
        [HttpGet]
        [Route("NewTrail")]
        public IActionResult NewTrail()
        {
            return View();
        }

        [HttpPost]
        [Route("AddTrail")]
        public IActionResult AddTrail(Trails trailsModel)
        {
            trailFactory.AddTrail(trailsModel);
            return View("NewTrail");
        }

        [HttpGet]
        [Route("ViewTrail/{Id}")]
        public IActionResult ViewTrail(int Id)
        {
            ViewBag.OneTrail = trailFactory.GetOneTrail(Id);
            return View();
        }

    }
}
