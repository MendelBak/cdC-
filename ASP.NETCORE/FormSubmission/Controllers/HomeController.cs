using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FormSubmission.Models;


namespace FormSubmission.Controllers
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
            ViewBag.errors = ModelState.Values;
            return View();
        }

        // Should not render from a POST route.
        [HttpPost]
        [Route("submitInfo")]
        public IActionResult submitInfo(string first_name, string last_name, int age, string email, string password)
        {
            User NewUser = new User
            {
            FirstName = first_name,
            LastName = last_name,
            Age = age,
            Email = email,
            Password = password,
            };
            if(TryValidateModel(NewUser) == true)
            {
                return View("success");
            }
            ViewBag.errors = ModelState.Values;
            return View("index");
        }
    }
}
