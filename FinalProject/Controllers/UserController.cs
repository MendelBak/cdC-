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
    public class UserController : Controller
    {

        private BeltExamContext _context;

        public UserController(BeltExamContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("register");
        }

        // Newuser route is the registration route for a new user.
        [HttpPost]
        [Route("NewUser")]
        public IActionResult NewUser(RegisterViewModel model)
        {
            // Check if models received any validation errors.
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if email already exists in DB.
                    var EmailExists = _context.Users.Where(e => e.Email == model.Email).SingleOrDefault();
                    if (EmailExists == null)
                    {
                        // Hash password
                        PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                        string HashedPassword = Hasher.HashPassword(model, model.Password);
                        User NewUser = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            Password = HashedPassword,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                        };
                        _context.Add(NewUser);
                        _context.SaveChanges();
                        // Set user id and first name in session for use in identification, future db calls, and for greeting the user.
                        HttpContext.Session.SetInt32("LoggedUserId", NewUser.UserId);
                        HttpContext.Session.SetString("LoggedUserName", NewUser.FirstName);
                        // Redirect to Account method.
                        return RedirectToAction("Account");
                    }
                    // Redirect w/ error if email already exists in db.
                    else
                    {
                        ViewBag.email = "That email is already in use. Please try again using another.";
                        return View("register");
                    }
                }
                // Catch should only run if there was an error with the db connection/query
                catch
                {
                    return View("register");
                }
            }
            return View("register");
        }


        [HttpGet]
        [Route("LoginPage")]
        public IActionResult LoginPage()
        {
            return View("login");
        }


        // This route handles login requests.
        [HttpPost]
        [Route("LoginSubmit")]
        public IActionResult LoginSubmit(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // The User object is being instantiated out here in order to establish it as a global variable and accessible by all the different try/catch statments. 
                User LoggedUser;

                // If there are no errors upon form submit check db for proper creds.
                LoggedUser = _context.Users.SingleOrDefault(u => u.Email == model.Email);

                // If block will run if matching email is not found in DB.
                if(LoggedUser == null)
                {
                    ViewBag.loginError = "Your email was incorrect or does not exist in our database.";
                    return View("login");
                }

                // If email is correct, verify that password is correct.
                var Hasher = new PasswordHasher<User>();

                // Check hashed password. 0 = negative match.
                if (Hasher.VerifyHashedPassword(LoggedUser, LoggedUser.Password, model.Password) != 0)
                {
                    // Set user id and first name in session for use in identification, future db calls, and for greeting the user.
                    HttpContext.Session.SetInt32("LoggedUserId", LoggedUser.UserId);
                    HttpContext.Session.SetString("LoggedUserName", LoggedUser.FirstName);
                    return RedirectToAction("Account");
                }
                // If password does not match.
                else
                {
                    ViewBag.loginError = "Your password was incorrect.";
                    return View("login");
                }
            }
            // If ModelState was illegal return login and display model validation errors.
            else
            {
                return View("login");
            }
        }


        [HttpGet]
        [Route("Account")]
        public IActionResult Account()
        {
            // Check to ensure there is a properly logged in user by checking session.
            if (HttpContext.Session.GetInt32("LoggedUserId") >= 0)
            {
                try
                {
                    // Save first name in session to display greeting on navbar.
                    ViewBag.FirstName = HttpContext.Session.GetString("LoggedUserName");

                    // Save id in session and then send to View using Viewbag
                    ViewBag.UserId = HttpContext.Session.GetInt32("LoggedUserId");

                    // Get User info to display and to determine whether or not the user is attending the activity.
                    var AccountInfo = _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("LoggedUserId")).SingleOrDefault();
                    ViewBag.AccountInfo = AccountInfo;

                    // Get all activities in order to display in Account View.
                    // List<Activity> AllActivities = _context.Activities.OrderBy(a => a.Date).ToList();

                    List<Activity> AllActivities = _context.Activities.Include(a => a.Admin).OrderBy(a => a.Date).ToList();
                    ViewBag.AllActivities = AllActivities;
                    


                    List<Subscription> AllSubscriptions = _context.Subscriptions.ToList();
                    // Get Subscriptions in order to determine how many guests are attending and whether current user is attending each activity.
                    ViewBag.AllSubscriptions = AllSubscriptions;


                    // var GuestCounter = 0;
                    // foreach (var x in AllActivities)
                    // {
                    //     for (var i = 0; i < x.ActivityId; i++)
                    //     {
                    //         GuestCounter = AllSubscriptions.Count(t => t.UserId > 0);
                    //     }


                    //     ViewBag.GuestCounter = GuestCounter;
                    // }

                    return View("Account");
                }
                // Catch should only fire if there was an error getting/setting sesion id and username to ViewBag but if session id exists (which means a user is logged in). Send to page without greeting on navbar.
                catch
                {
                    return View("Account");
                }
            }
            // If no id is in session that means that the user is not properly logged on. Redirect to logout which will end up at LoginPage.
            return RedirectToAction("Logout");
        }


        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage");
        }







    }
}
