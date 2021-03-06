using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Controllers
{
    public class ActivityController : Controller
    {

        private BeltExamContext _context;

        public ActivityController(BeltExamContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("PlanActivity")]
        public IActionResult PlanActivity()
        {
            // Check to ensure there is a properly logged in user by checking session.
            if (HttpContext.Session.GetInt32("LoggedUserId") >= 0)
            {
                return View("PlanActivity");
            }
            else
            {
                ViewBag.LoginError = "You are not logged in. Please log in to plan a new Activity.";
                return View("Account");
            }
        }


        [HttpPost]
        [Route("NewActivity")]
        public IActionResult NewActivity(ActivityViewModel model)
        {
            // Check to ensure form data is legal.
            if (ModelState.IsValid)
            {
                // Check to ensure Activity date is in the future.
                if (model.Date > DateTime.Now)
                {
                    // Insert Activity data into db.
                    try
                    {
                        // Set sessionId to be AdminId in db.
                        int? AdminId = HttpContext.Session.GetInt32("LoggedUserId");
                        Activity NewActivity = new Activity
                        {
                            AdminId = (int)AdminId,
                            Title = model.Title,
                            Time = model.Time,
                            Date = model.Date,
                            Duration = model.Duration,
                            DurationType = model.DurationType,
                            Description = model.Description,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                        };
                        // Add new Activity to DB.
                        _context.Activities.Add(NewActivity);
                        _context.SaveChanges();

                        // Get the ActivityId in order to redirect to the proper page. 
                        var CurrentActivity = _context.Activities.Where(w => w.AdminId == AdminId).LastOrDefault();
                        
                        // After successful creation of new Activity, redirect to that Activity's specific page.
                        return RedirectToAction("ActivityDetails", new { ActivityId = CurrentActivity.ActivityId });
                    }
                    // Catch should only run if insertion of data into DB failed. 
                    catch
                    {
                        ViewBag.ActivityErrors = "Oh no! There was an error saving your new activity. Please try again.";
                        return View("PlanActivity");
                    }
                }
                else
                {
                    ViewBag.ActivityErrors = "The date of the activity must be in the future, not the past.";
                    return View("PlanActivity");
                }
            }
            // This route will be followed if the ModelState is invalid. It will return to the PlanActivity page and display the proper model/form errors.
            else
            {
                return View("PlanActivity");
            }
        }

        // Shows details of a specific activity.
        [HttpGet]
        [Route("ActivityDetails/{ActivityId}")]
        public IActionResult ActivityDetails(int ActivityId)
        {
            // Get Activity in order to display in ActivityDetails View.
            Activity OneActivity = _context.Activities.Where(w => w.ActivityId == ActivityId).Include(g => g.Guests).ThenInclude(u => u.User).SingleOrDefault();
            ViewBag.OneActivity = OneActivity;

            // Check (on frontend) if user is admin(ability to delete event).
            int? LoggedUserId = HttpContext.Session.GetInt32("LoggedUserId");
            ViewBag.LoggedUserId = LoggedUserId;

            // Get subscription in order to check if Current User is attending and whether to display Leave/Join button.
            List<Subscription> OneSubscription = _context.Subscriptions.Where(s => s.ActivityId == ActivityId && s.UserId == LoggedUserId).ToList();
            ViewBag.OneSubscription = OneSubscription;

            return View("ActivityDetails");
        }


        // Add single user to activity Guest List.
        [HttpGet]
        [Route("JoinActivity/{ActivityId}")]
        public IActionResult JoinActivity(int ActivityId)
        {
            // Get UserId to add Subscription table to Join activity.
            int? LoggedUserId = HttpContext.Session.GetInt32("LoggedUserId");

            Activity OneActivity = _context.Activities.Where(w => w.ActivityId == ActivityId).SingleOrDefault();
            ViewBag.OneActivity = OneActivity;

            // Get subscription in order to display in ActivityDetails View
            List<Subscription> OneSubscription = _context.Subscriptions.Where(s => s.ActivityId == ActivityId && s.UserId == LoggedUserId).ToList();

            if (OneSubscription.Count > 0)
            {
                return RedirectToAction("Account", "User");
            }



            Subscription NewSubscription = new Subscription
            {
                UserId = (int)LoggedUserId,
                ActivityId = OneActivity.ActivityId,
            };
            _context.Subscriptions.Add(NewSubscription);
            _context.SaveChanges();

            return RedirectToAction("ActivityDetails", new { ActivityId = OneActivity.ActivityId });
        }


        // Remove single user from activity Guest List.
        [HttpGet]
        [Route("LeaveActivity/{ActivityId}")]
        public IActionResult LeaveActivity(int ActivityId)
        {
            // Get activity in order to display in ActivityDetails View.
            Activity OneActivity = _context.Activities.Where(w => w.ActivityId == ActivityId).SingleOrDefault();
            ViewBag.OneActivity = OneActivity;

            // Get UserId to know which user is Leaving activity.
            int? LoggedUserId = HttpContext.Session.GetInt32("LoggedUserId");

            // Query to get the subscription for the guest to be dropped from DB.
            Subscription CurrentSubscription = _context.Subscriptions.FirstOrDefault(s => s.UserId == LoggedUserId);

            _context.Subscriptions.Remove(CurrentSubscription);
            _context.SaveChanges();

            return RedirectToAction("Account", "User");
        }


        // Delete activity from DB.
        [HttpGet]
        [Route("DeleteActivity/{ActivityId}")]
        public IActionResult DeleteActivity(int ActivityId)
        {
            // Get LoggedUserId to perform second check (one on front end) to compare to AdminId on designated activity to ensure that user has admin creds.
            int? LoggedUserId = HttpContext.Session.GetInt32("LoggedUserId");
            Activity OneActivity = _context.Activities.Where(w => w.ActivityId == ActivityId).SingleOrDefault();
            // Comparison of creds.
            if (LoggedUserId == OneActivity.AdminId)
            {
                _context.Activities.Remove(OneActivity);
                _context.SaveChanges();
                return RedirectToAction("Account", "User");
            }
            // Else block will run if the user who attempted to delete this activity is not authorized to do so.
            else
            {
                ViewBag.DeleteError = "Sorry, we were unable to delete that activity. Please try again or try logging in with the account that is the Administrator of this activity.";
                return View("ActivityDetails", new { ActivityId = OneActivity.ActivityId });
            }
        }


    }
}


