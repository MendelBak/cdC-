using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RESTauranter.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RESTauranter.Controllers
{

    public class HomeController : Controller
    {
        private ReviewContext _context;

        public HomeController(ReviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("NewReview")]
        public IActionResult NewReview(Review reviewModel)
        {
            if(ModelState.IsValid && reviewModel.Date_Of_Visit < DateTime.Now)
            {
                _context.Add(reviewModel);
                _context.SaveChanges();
                return RedirectToAction("Reviews");
            }
            else
            {
                ViewBag.DateTimeError = "Your Date of Visit must be in the past";
                return View("Index");
            }
        }

        [HttpGet]
        [Route("reviews")]
        public IActionResult Reviews()
        {
            List<Review> AllReviews = _context.Reviews.ToList();
            ViewBag.reviews = AllReviews;
            return View("reviews");
        }
    }

}
