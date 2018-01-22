using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using loginReg.Models;

namespace loginReg.Controllers
{

    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;
        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.email = "";
            return View();
        }

        [HttpPost]
        [Route("submitInfo")]
        public IActionResult submitInfo(AllUsers model)
        {
            // CHeck if models received any validation errors.
            if (ModelState.IsValid)
            {
                // Check if email already exists in DB.
                string CheckEmail = $"SELECT email FROM users WHERE email = '{model.Reg.Email}'";
                var EmailExists = _dbConnector.Query(CheckEmail);
                if (EmailExists.Count <= 0)
                {
                    string NowTime = "NOW()";
                    _dbConnector.Execute($"INSERT INTO users (first_name, last_name, email, password, created_at, updated_at) VALUES ('{model.Reg.FirstName}', '{model.Reg.LastName}', '{model.Reg.Email}', '{model.Reg.Password}',{NowTime}, {NowTime}) ");
                    ViewBag.email = "";
                    return View("success");
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
            string LoginQuery = $"SELECT id FROM users WHERE email = '{model.Log.Email}' AND password = '{model.Log.Password}'";
            var CheckLogin = _dbConnector.Query(LoginQuery);
            if (CheckLogin.Count > 0)
            {
                // int? UserID = HttpContext.Session.SetInt32(id);
                return View("success");
            }
            else
            {
                ViewBag.loginError = "Username or password was incorrect. Please try again.";
                return View("index");
            }
        }
    }
}
