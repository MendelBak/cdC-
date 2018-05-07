using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using dojodachi;


namespace dojodachi.Controllers
{
    public class HomeController : Controller
    {
        // Random Generator is defined up here for use in multiple methods below
        static Random rand = new Random();
                
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObjectFromJson<dachi>("DachiData") == null)
            {
                HttpContext.Session.SetObjectAsJson("DachiData", new dachi());
            }

            // Retrieve Dachi and set it into ViewBag to display on Front End
            dachi myDachi = HttpContext.Session.GetObjectFromJson<dachi>("DachiData");
            ViewBag.myDachi = myDachi;

            if (ViewBag.myDachi.fullness < 1 || ViewBag.myDachi.happiness < 1)
            {
                ViewBag.myDachi.actionMessage = "Your Dachi just died...";
                myDachi.loseStatus = true;
                HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
            }
            if (ViewBag.myDachi.fullness > 100 && ViewBag.myDachi.happiness > 100 && ViewBag.myDachi.energy > 100)
            {
                myDachi.winStatus = true;
                ViewBag.myDachi.actionMessage = "Your Dachi has accomplished its task in life! You Win!";
                HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
            }

            return View();
        }

        [HttpGet]
        [Route("Feed")]
        public JsonResult Feed()
        {
            // Retrieve dachi
            dachi myDachi = HttpContext.Session.GetObjectFromJson<dachi>("DachiData");

            // Check if feed action is permitted
            if (myDachi.meals < 1)
            {
                myDachi.actionMessage = "You cannot feed your Dachi. You have no meals left!";
                HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
                return Json(myDachi);
            }

            // 1/4th chance that Dachi won't like the meal
            if (rand.Next(0, 5) == 0)
            {
                myDachi.meals -= 1;
                myDachi.actionMessage = "Oh no! Your Dachi didn't like the meal you served.";
                HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
                return Json(myDachi);
            }

            // Manipulate dachi values
            myDachi.meals -= 1;
            int fullnessGained = rand.Next(5, 11);
            myDachi.fullness += fullnessGained;
            myDachi.actionMessage = $"You fed your Dachi and it gained {fullnessGained} fullness!";

            // Save dachi in session
            HttpContext.Session.SetObjectAsJson("DachiData", myDachi);

            // Send dachi to front end
            ViewBag.DachiData = myDachi;
            return Json(myDachi);
        }

        [HttpGet]
        [Route("Play")]
        public JsonResult Play()
        {
            // Retrieve dachi
            dachi myDachi = HttpContext.Session.GetObjectFromJson<dachi>("DachiData");

            // Check if feed action is permitted
            if (myDachi.energy < 5)
            {
                myDachi.actionMessage = "You can't play with your Dachi. Your energy is too low!";
                HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
                return Json(myDachi);
            }

            // 1/4 chance that the Dachi won't want to play
            if(rand.Next(0,5) == 0)
            {
                myDachi.energy -= 5;
                myDachi.actionMessage = "Your Dachi doesn't want to play right now...";
                HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
                return Json(myDachi);
            }

            // Manipulate Dachi values
            myDachi.energy -= 5;
            int happinessGained = rand.Next(5, 11);
            myDachi.happiness += happinessGained;
            myDachi.actionMessage = $"You played with your Dachi and it gained {happinessGained} Happiness!";

            // Save dachi in session
            HttpContext.Session.SetObjectAsJson("DachiData", myDachi);

            // Send dachi to front end
            return Json(myDachi);
        }

        [HttpGet]
        [Route("Work")]
        public JsonResult Work()
        {
            // Retrieve dachi
            dachi myDachi = HttpContext.Session.GetObjectFromJson<dachi>("DachiData");

            if(myDachi.energy < 5)
            {
                myDachi.actionMessage = "Your Dachi is too tired to work...";
                HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
                return Json(myDachi);
            }

            int mealsGained = rand.Next(1,4);
            myDachi.meals += mealsGained;
            myDachi.energy -= 5;
            myDachi.actionMessage = $"Your Dachi worked really hard and earned {mealsGained} Meals!";
            HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
            return Json(myDachi);
        }

        [HttpGet]
        [Route("Sleep")]
        public JsonResult Sleep()
        {
            dachi myDachi = HttpContext.Session.GetObjectFromJson<dachi>("DachiData");

            if(myDachi.fullness < 5 || myDachi.happiness < 5)
            {
                myDachi.actionMessage = "You cannot go to sleep with a hungry or a sad Dachi!";
                HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
                return Json(myDachi);
            }
            myDachi.happiness -= 5;
            myDachi.fullness -= 5;
            myDachi.energy += 15;
            myDachi.actionMessage = "Your Dachi took a nap and gained 15 Energy!";
            HttpContext.Session.SetObjectAsJson("DachiData", myDachi);
            return Json(myDachi);
        }

        [HttpGet]
        [Route("Clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


    }

    // This extension enables Session to hold objects instead of just ints and strings.
    public static class SessionExtensions
    {
        // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes theobject to JSON and stores it as a string in session
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upone retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}