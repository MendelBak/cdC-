using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheWall.Models;
using DbConnection;

namespace TheWall.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("index");
        }


        [HttpGet]
        [Route("main")]
        public IActionResult Main()
        {
            if (HttpContext.Session.GetInt32("UserID") >= 0)
            {
                // Retreive all messages to display in View.
                string GetMessages = "SELECT messages.message, messages.created_at, users.first_name, users.last_name FROM messages JOIN users ON messages.users_id = users.id;";
                ViewBag.AllMessages = DbConnector.Query(GetMessages);

                return View("main");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // submitInfo route is the registration route for a new user.
        [HttpPost]
        [Route("submitInfo")]
        public IActionResult submitInfo(AllUsers model)
        {
            // CHeck if models received any validation errors.
            if (ModelState.IsValid)
            {
                // Check if email already exists in DB.
                string CheckEmail = $"SELECT email FROM users WHERE email = '{model.Reg.Email}'";
                var EmailExists = DbConnector.Query(CheckEmail);
                if (EmailExists.Count <= 0)
                {
                    string NowTime = "NOW()";
                    DbConnector.Execute($"INSERT INTO users (first_name, last_name, email, password, created_at, updated_at) VALUES ('{model.Reg.FirstName}', '{model.Reg.LastName}', '{model.Reg.Email}', '{model.Reg.Password}',{NowTime}, {NowTime}) ");
                    ViewBag.email = "";
                    return RedirectToAction("main");
                }
                else if (EmailExists.Count > 0)
                {
                    ViewBag.email = "There is already a user with that email address. Please try again.";
                    return View("index");
                }
                else
                {
                    ViewBag.email = "";
                    return View("index");
                }
            }
            else
            {
                ViewBag.email = "";
                return View("index");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(AllUsers model)
        {
            var CheckLogin = DbConnector.Query($"SELECT first_name, id FROM users WHERE email = '{model.Log.Email}' AND password = '{model.Log.Password}';");
            if (CheckLogin.Count > 0)
            {
                // Save first name in session to display greeting on navbar.
                HttpContext.Session.SetString("FirstName", (string)CheckLogin[0]["first_name"]);
                TempData["FirstName"] = HttpContext.Session.GetString("FirstName");
                // Save id in session and then send to View using TempData
                HttpContext.Session.SetInt32("UserID", (int)CheckLogin[0]["id"]);
                TempData["UserID"] = HttpContext.Session.GetInt32("UserID");

                return RedirectToAction("main");
            }
            else
            {
                ViewBag.loginError = "Username or password was incorrect. Please try again.";
                return View("index");
            }
        }

        [HttpPost]
        [Route("PostSubmit")]
        public IActionResult PostSubmit(string message)
        {
            string TempMessage = '"' + message + '"';
            // User Id was stored in session upon login.
            int? UserID = HttpContext.Session.GetInt32("UserID");
            string NowTime = "NOW()";
            string MessageQuery = $"INSERT INTO messages (users_id,message, created_at, updated_at) VALUES ({UserID}, {TempMessage},  {NowTime}, {NowTime})";
            DbConnector.Execute(MessageQuery);
            return RedirectToAction("main");
        }

        [HttpPost]
        [Route("CommentSubmit")]
        public IActionResult CommentSubmit(string comment)
        {
            string Comment = '"' + comment + '"';
            return RedirectToAction("main");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
    }
}
