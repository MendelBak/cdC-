using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using formSubmission.Models;

namespace formSubmission.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.errors = "";
            return View();
        }

        [HttpPost]
        [Route("FormSubmit")]
        public IActionResult FormSubmit(string firstName, string lastName, int age, string email, string password)
        {
            User NewUser = new User
            {
                firstName = firstName,
                lastName = lastName,
                age = age,
                email = email,
                password = password

            };
            TryValidateModel(NewUser);
            ViewBag.errors = ModelState.Values;
            if(ModelState.IsValid)
            {
                return View("Success");
            }
            return View("Index");
        }
    }
}
