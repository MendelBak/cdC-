using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTraunter.Models;
using Microsoft.EntityFrameworkCore;

namespace RESTraunter.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantContext _context;

        public HomeController(RestaurantContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("SubmitReview")]
        public IActionResult SubmitReview(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                Reviews NewReview = new Reviews
                {
                    ReviewerName = model.ReviewerName,
                    RestaurantName = model.RestaurantName,
                    Review = model.Review,
                    Stars = model.Stars,
                    DateOfVisit = model.DateOfVisit,
                    Helpful = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now                    
                };
                _context.Reviews.Add(NewReview);
                _context.SaveChanges();
                return RedirectToAction("Reviews");
            }
            return View("Index");
        }

        [HttpGet]
        [Route("Reviews")]
        public IActionResult Reviews()
        {
            ViewBag.AllReviews = _context.Reviews.ToList().OrderByDescending(r => r.Helpful);
            ViewBag.HelpfulReviewResponse = "";
            return View();
        }

        [HttpGet]
        [Route("HelpfulReview/{id}/{response}")]
        public IActionResult HelpfulReview(int id, bool response)
        {
            Reviews CurrentReview = _context.Reviews.SingleOrDefault( review => review.Id == id);
            if(response == true) 
            {
                CurrentReview.Helpful++;
            }
            else 
            {
                CurrentReview.Helpful--;
            }
            _context.SaveChanges();
            ViewBag.HelpfulReviewResponse = "Thank you for your input!";
            return RedirectToAction("Reviews");
        }
    }
}
