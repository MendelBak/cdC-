using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using theWall.Models;

namespace theWall.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;

        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }


        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.RegisterError = "";
            return View();
        }

        [HttpPost]
        [Route("NewUser")]
        public IActionResult NewUser(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                // Check if Username already exists
                List<Dictionary<string, object>> CheckUsernameExists = _dbConnector.Query($"SELECT username FROM users WHERE username = '{registerViewModel.Username}'");
                if (CheckUsernameExists.Count > 0)
                {
                    ViewBag.RegisterError = "Can't register new user. Username already exists.";
                    System.Console.WriteLine("Can't register new user. Username already exists.");
                    return View("Index");
                }
                // Insert into DB if username doesn't already exist.
                _dbConnector.Execute($"INSERT INTO users (username, password, created_at) VALUES ('{registerViewModel.Username}', '{registerViewModel.Password}', NOW())");
                System.Console.WriteLine("Successfully added new user into database.");

                // Redirect user to login
                return View("Login");
            }
            System.Console.WriteLine("New user failed due to user input error. Did not pass UserViewModel validations");
            return View("Index");
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            // Used to pass login error messages that cannot be passed via ViewModel validations.
            ViewBag.LoginErrors = "";
            return View("Login");
        }

        // These login/register functions are not secure. You can totally get around them. The point is not for security sake but rather to learn simply and quickly.
        [HttpPost]
        [Route("LoginSubmit")]
        public IActionResult LoginSubmit(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                // Check creds
                List<Dictionary<string, object>> CurrentUser = _dbConnector.Query($"SELECT * FROM users WHERE username='{loginViewModel.Username}' AND password='{loginViewModel.Password}'");
                if (CurrentUser.Count > 0)
                {
                    HttpContext.Session.SetInt32("CurrentUser", (int)CurrentUser[0]["id"]);
                    System.Console.WriteLine("User successfully logged in");
                    return RedirectToAction("Main");
                }
                ViewBag.LoginErrors = "Incorrect username or password";
                System.Console.WriteLine("Incorrect username or password");
                return View("Login");
            }
            System.Console.WriteLine("DB query never fired due to user input error. Did not pass LoginViewModel validations");
            return View("Login");
        }

        [HttpGet]
        [Route("Main")]
        public IActionResult Main()
        {
            ViewBag.AllMessages = _dbConnector.Query("SELECT * FROM messages ORDER BY created_at DESC");
            ViewBag.AllComments = _dbConnector.Query("SELECT * FROM comments");
            System.Console.WriteLine(ViewBag.AllMessages[0]["users_id"]);
            return View("Main");
        }

        [HttpPost]
        [Route("NewPost")]
        public IActionResult NewPost(MessageViewModel messageViewModel)
        {
            if (ModelState.IsValid)
            {
                _dbConnector.Execute($"INSERT INTO messages (message, users_id, created_at, updated_at) VALUES('{messageViewModel.Message}', {HttpContext.Session.GetInt32("CurrentUser")}, NOW(), NOW())");
                TempData["MessageConfirm"] = "Youve succesfully posted your message!";
            }
            return RedirectToAction("Main");
        }


        [HttpPost]
        [Route("NewComment")]
        public IActionResult NewComment(CommentViewModel commentViewModel, int MessageId, int MessagePosterId)
        {
            if (ModelState.IsValid)
            {
                // Insert comment into DB
                _dbConnector.Execute($"INSERT INTO comments (comment, users_id, messages_id, messages_users_id,created_at, updated_at) VALUES('{commentViewModel.Comment}', {HttpContext.Session.GetInt32("CurrentUser")}, {MessageId}, {MessagePosterId}, NOW(), NOW())");
                
                // Used Temp Data since we're redirecting
                TempData["CommentConfirm"] = "Youve succesfully posted your comment!";
            }
            return RedirectToAction("Main");
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
