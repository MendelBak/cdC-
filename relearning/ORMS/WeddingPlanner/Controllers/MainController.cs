using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// Grants access to the PasswordHasher function
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class MainController : Controller
    {
        private WeddingContext _context;

        public MainController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Account")]
        public IActionResult Account()
        {
            var CurrentUser = _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("CurrentUserId")).Include(w => w.WeddingsAttending).ThenInclude(x => x.Weddings).SingleOrDefault();

            if (CurrentUser == null)
            {
                ViewBag.LoginError = "Sorry, a problem occured while attempting to grab your account. Please login again.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.CurrentUser = CurrentUser;

                // Get all Weddings to display in table
                ViewBag.AllWeddings = _context.Weddings.Where(w => w.Bride == w.Bride).ToList();
                return View("Main");
            }
        }

        [HttpGet]
        [Route("NewWedding")]
        public IActionResult NewWedding()
        {

            return View("NewWedding");
        }

        [HttpPost]
        [Route("SubmitNewWedding")]
        public IActionResult SubmitNewWedding(WeddingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Weddings NewWedding = new Weddings
                {
                    AdminId = (int)HttpContext.Session.GetInt32("CurrentUserId"),
                    Bride = model.Bride,
                    Groom = model.Groom,
                    Address = model.Address,
                    Date = model.Date,
                    NumGuests = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                _context.Weddings.Add(NewWedding);
                _context.SaveChanges();
            }
            return RedirectToAction("Account");
        }

        [HttpGet]
        [Route("ShowWedding/{WeddingId}")]
        public IActionResult ShowWedding(int WeddingId)
        {
            try
            {
                var CurrentWedding = _context.Weddings.Where(w => w.WeddingsId == WeddingId).Include(a => a.Atendees).ThenInclude(u => u.User).SingleOrDefault();
                ViewBag.CurrentWedding = CurrentWedding;
                return View("ShowWedding");
            }
            catch
            {
                return RedirectToAction("Account");
            }
        }

        [HttpGet]
        [Route("JoinWedding/{WeddingId}")]
        public IActionResult JoinWedding(int WeddingId)
        {
            // Get user object
            var CurrentUser = _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("CurrentUserId")).Include(w => w.WeddingsAttending).ThenInclude(x => x.Weddings).SingleOrDefault();
            
            // Change UpdatedAt field in User object
            CurrentUser.UpdatedAt = DateTime.Now;

            // Check if user is already attending the wedding.
            // if(CurrentUser.WeddingsAttending.WeddingsId == WeddingId)
            // {

            // }

            // Get wedding object
            Weddings SelectedWedding = _context.Weddings.Where(w => w.WeddingsId == WeddingId).SingleOrDefault();
            SelectedWedding.NumGuests++;

            // Add user to weddings attending list.
            Atendees NewAtendee = new Atendees
            {
                User = CurrentUser,
                Weddings = SelectedWedding
            };
            _context.Atendees.Add(NewAtendee);
            _context.SaveChanges();
            return RedirectToAction("ShowWedding", new { WeddingId = WeddingId });
        }

        [HttpGet]
        [Route("Delete/{WeddingId}")]
        public IActionResult Delete(int WeddingId)
        {
            int? CurrentUserId = HttpContext.Session.GetInt32("CurrentUserId");

            // Get wedding if creds match
            Weddings SelectedWedding = _context.Weddings.Where(w => w.AdminId == (int)CurrentUserId && w.WeddingsId == WeddingId).SingleOrDefault();

            if (SelectedWedding.Bride != SelectedWedding.Bride)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                _context.Weddings.Remove(SelectedWedding);
                _context.SaveChanges();
                return RedirectToAction("Account");
            }
        }


    }
}