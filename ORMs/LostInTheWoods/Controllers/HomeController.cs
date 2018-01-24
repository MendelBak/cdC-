using System;
using System.Collections.Generic;
using LostInTheWoods.Models;
using Microsoft.AspNetCore.Mvc;
using LostInTheWoods.Factory; //Need to include reference to new Factory Namespace
namespace LostInTheWoods.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrailFactory trailFactory;
        public HomeController()
        {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            trailFactory = new TrailFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            //We can call upon the methods of the userFactory directly now.
            ViewBag.AllTrails = trailFactory.FindAll();
            return View();
        }
        

        [HttpGet]
        [Route("newtrail")]
        public IActionResult NewTrail()
        {
            return View("newtrail");
        }


        [HttpGet]
        [Route("trails/{id}")]
        public IActionResult Trails(int id)
        {
            ViewBag.Trail = trailFactory.FindByID(id);
            return View("trails");
        }

        [HttpPost]
        [Route("SubmitTrail")]
        public IActionResult SubmitTrail(Trail trail)
        {
            trailFactory.Add(trail);
            return RedirectToAction("index");
        }
    }
}
