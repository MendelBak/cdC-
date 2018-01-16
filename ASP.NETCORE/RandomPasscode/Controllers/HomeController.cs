using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // Generate random string 
            Random rand = new Random();
            string DataPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            ViewBag.RandString = "";
            for(int idx = 0; idx < 14; idx++)
            {
            int newIdx = rand.Next(DataPool.Length);
            ViewBag.RandString += DataPool[newIdx];
            }

            // Set session counter
            int? Counter = HttpContext.Session.GetInt32("Counter");
            if(Counter == null)
            {
            HttpContext.Session.SetInt32("Counter", 1);
            }
            else
            {
                Counter += 1;
                HttpContext.Session.SetInt32("Counter", (int)Counter);
            }
            TempData["Counter"] = HttpContext.Session.GetInt32("Counter");
            
            return View("index");
        }

        [HttpGet]
        [Route("generate")]
        public RedirectToActionResult Generate() 
        {
            return RedirectToAction ("Index");
        }
        [HttpGet]
        [Route("reset")]
        public RedirectToActionResult Reset() 
        {
            HttpContext.Session.SetInt32("Counter", 0);
            return RedirectToAction ("Index");
        }


    }
}