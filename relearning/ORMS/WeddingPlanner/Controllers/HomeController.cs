using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// Grants access to the PasswordHasher function
using Microsoft.AspNetCore.Identity;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{

    public class HomeController : Controller
    {
        private WeddingContext _context;

        public HomeController(WeddingContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // ViewBag.LoginError holds errors when users input incorrect credentials when attempting to login.
            ViewBag.LoginError = "";
            // ViewBag.Register holds errors when there was a DB insertion error upon registration.
            ViewBag.RegisterError = "";
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserViewModels model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if email already exists in DB.
                    User EmailExists = _context.Users.Where(u => u.Email == model.RegVM.Email).SingleOrDefault();
                    if (EmailExists != null)
                    {
                        // Return error to users telling them that the email is already in the DB.
                        ViewBag.RegisterError = "That email is already in our database. Try logging in or using a different email account.";
                        return View("Index");
                    }
                    // If email is not found in DB, continue with registration.

                    // Create new Hasher object
                    PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                    // Create new User object and save into DB.
                    User NewUser = new User
                    {
                        FirstName = model.RegVM.FirstName,
                        LastName = model.RegVM.LastName,
                        Email = model.RegVM.Email,
                        // In the Hashing function the First arguement is WHERE the hashing function is looking (In this case it is looking in the RegisterViewModel that is nested inside a wrapper model called UserViewModel aka 'model' in this method. Look at the method's parameters.). 
                        // The second arguement is WHICH attribute it is looking for.
                        Password = Hasher.HashPassword(model.RegVM, model.RegVM.Password),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now                        
                    };
                    _context.Add(NewUser);
                    _context.SaveChanges();
                    // Set user id in session.
                    HttpContext.Session.SetInt32("CurrentUserId", NewUser.UserId);
                    return RedirectToAction("Account", "Main");
                }
                // Catch should only run if there was a problem saving the user to the DB.
                catch
                {
                    ViewBag.RegisterError = "There was an error creating your new account. Please try again.";
                }
            }
            // This will run if the ModelState was invalid. Errors will pass through asp-validation-for on the front-end.
            return View("Index");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserViewModels model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Create new Hasher object.
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();

                    // Retrieve user from DB where submitted email matches.
                    User CurrentUser = _context.Users.Where(u => u.Email == model.LogVM.Email).SingleOrDefault();

                    // Check against user submitted password. Hash verification function will return 0 for a negative match.
                    if (0 != Hasher.VerifyHashedPassword(CurrentUser, CurrentUser.Password, model.LogVM.Password))
                    {
                        // Set user id in session.
                        HttpContext.Session.SetInt32("CurrentUserId", CurrentUser.UserId);
                        // Send user to Account controller and method.
                        return RedirectToAction("Account", "Main");
                    }
                    // if the password verification fails (user error) return error message.
                    ViewBag.LoginError = "Your password was incorrect. Please try again.";
                    return View("Index");
                }
                // If no match with email in DB or if can't contact DB.
                catch
                {
                    ViewBag.LoginError = "You entered an incorrect email or there is a problem communicating with our Database. Please try again.";
                    return View("Index");
                }
            }
            // Returns validation errors if ModelState is invalid.
            return View("Index");
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
