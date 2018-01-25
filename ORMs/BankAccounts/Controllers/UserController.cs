using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using BankAccounts.Models;

namespace BankAccounts.Controllers
{
    public class UserController : Controller
    {

        private BankContext _context;

        public UserController(BankContext context)
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
                        User NewUser = new User
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            Password = model.Password,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                        };
                        _context.Add(NewUser);
                        _context.SaveChanges();
                        // Set user id and first name in session for use in identification, future db calls, and for greeting the user.
                        HttpContext.Session.SetInt32("LoggedUserId", NewUser.UserId);
                        HttpContext.Session.SetString("LoggedUserName", NewUser.FirstName);
                        // Redirect to Account method in Account controller.
                        return RedirectToAction("Account", "Account");
                    }
                    // Redirect w/ error if email already exists in db.
                    else
                    {
                        ViewBag.email = "That email is already in use. Please try again using another.";
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
        public IActionResult LoginPage(LoginViewModel model)
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
                // If no errors on form submit check db for proper creds.
                try
                {
                    User LoggedUser = _context.Users.SingleOrDefault(u => u.Email == model.Email);
                    // Set user id and first name in session for use in identification, future db calls, and for greeting the user.
                    HttpContext.Session.SetInt32("LoggedUserId", LoggedUser.UserId);
                    HttpContext.Session.SetString("LoggedUserName", LoggedUser.FirstName);
                    return RedirectToAction("Account", "Account");
                }
                // If no proper creds redirect to login page and return error.
                catch
                {
                    ViewBag.loginError = "Sorry, your email or password were incorrect.";
                    return View("login");
                }
            }
            // If form submit was illegal redirect to login and display model validation errors.
            else
            {
                return View("login");
            }
        }






    }
}
