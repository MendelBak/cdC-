using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace randomPasscode.Controllers
{
    public class HomeController :Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        [Route("Generate")]
        public JsonResult Generate()
        {
            int? attemptNum = HttpContext.Session.GetInt32("attemptNum");
            if(attemptNum == null)
            {
                HttpContext.Session.SetInt32("attemptNum", 1);
            } 
            else
            {
                attemptNum += 1;
                HttpContext.Session.SetInt32("attemptNum", (int)attemptNum);
            }
            Random rand = new Random();
            string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
            string randomString = "";
            for(int i = 0; i < alphabet.Length; i++)
            {
                int randInt = rand.Next(alphabet.Length - 1);
                randomString += alphabet[randInt];
            }

            // Convert results to anonymous object.
            var result = new
            {
                passcode = randomString,
                attempts = HttpContext.Session.GetInt32("attemptNum")
            };

            // Convert anonymous object to a JSON object and return it to the view that called it (Index).
            return Json(result);
        }
        
        [HttpGet]
        [Route("Clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}