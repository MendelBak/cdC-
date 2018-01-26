using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {

        private WeddingContext _context;

        public WeddingController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("PlanWedding")]
        public IActionResult PlanWedding()
        {
            // Check to ensure there is a properly logged in user by checking session.
            if (HttpContext.Session.GetInt32("LoggedUserId") >= 0)
            {
                return View("PlanWedding");
            }
            else
            {
                ViewBag.LoginError = "You are not logged in. Please log in to plan a new wedding.";
                return View("Account");
            }
        }

        [HttpPost]
        [Route("NewWedding")]
        public IActionResult NewWedding(WeddingViewModel model)
        {
            // Check to ensure form data is legal.
            if (ModelState.IsValid)
            {
                // Check to ensure wedding date is in the future.
                if (model.DateOfWedding > DateTime.Now)
                {
                    // Insert wedding data into db.
                    try
                    {
                        // Set sessionId to be AdminId in db.
                        int? AdminId = HttpContext.Session.GetInt32("LoggedUserId");
                        Wedding NewWedding = new Wedding
                        {
                            AdminId = (int)AdminId,
                            WifeName = model.WifeName,
                            HusbandName = model.HusbandName,
                            Address = model.Address,
                            DateOfWedding = model.DateOfWedding,
                            CreatedAt = DateTime.Now,
                        };
                        // Add new wedding to DB.
                        _context.Weddings.Add(NewWedding);
                        // Get the WeddingId in order to redirect to the proper page. 
                        var CurrentWedding = _context.Weddings.Where(w => w.AdminId == AdminId).LastOrDefault();
                        _context.SaveChanges();

                        // After successful creation of new wedding redirect to that wedding's specific page.
                        return RedirectToAction("WeddingDetails", new {WeddingId = CurrentWedding.WeddingId});
                    }
                    // Catch should only run if insertion of data into DB failed. 
                    catch
                    {
                        ViewBag.WeddingErrors = "Oh No! There was an error saving your new wedding. Please try again.";
                        return View("PlanWedding");
                    }
                }
                else
                {
                    ViewBag.WeddingErrors = "We know you want to get married soon but the date of the Wedding must be in the future, not the past.";
                    return View("PlanWedding");
                }
            }
            else
            {
                return View("PlanWedding");
            }
        }

        [HttpGet]
        [Route("WeddingDetails/{WeddingId}")]
        public IActionResult WeddingDetails(int WeddingId)
        {
            // Get wedding in order to display in WeddingDetails View.
            var OneWedding = _context.Weddings.Where(w => w.WeddingId == WeddingId);
            ViewBag.OneWedding = OneWedding;
            return View("WeddingDetails");
        }




    }
}
