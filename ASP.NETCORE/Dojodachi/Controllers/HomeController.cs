using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // Set game status
            TempData["GameStatus"] = "playing";

            // Check if we need to set defaults or if already in middle of a game.
            int? Happiness = HttpContext.Session.GetInt32("Happiness");
            if (Happiness == null)
            {
                HttpContext.Session.SetInt32("Happiness", 20);
            }

            int? Fullness = HttpContext.Session.GetInt32("Fullness");
            if (Fullness == null)
            {
                HttpContext.Session.SetInt32("Fullness", 20);
            }

            int? Energy = HttpContext.Session.GetInt32("Energy");
            if (Energy == null)
            {
                HttpContext.Session.SetInt32("Energy", 50);
            }

            int? Meals = HttpContext.Session.GetInt32("Meals");
            if (Meals == null)
            {
                HttpContext.Session.SetInt32("Meals", 3);
            }
            TempData["Happiness"] = HttpContext.Session.GetInt32("Happiness");
            TempData["Fullness"] = HttpContext.Session.GetInt32("Fullness");
            TempData["Energy"] = HttpContext.Session.GetInt32("Energy");
            TempData["Meals"] = HttpContext.Session.GetInt32("Meals");
            if (Energy <= 0 || Happiness <= 0)
            {
                TempData["GameStatus"] = "over";
                TempData["Message"] = "You lost the game. Try again!";
            }
            if (Energy >= 100 && Fullness >= 100 && Happiness >= 100)
            {
                TempData["Message"] = "You won! Good job!";
                TempData["GameStatus"] = "over";
            }
            return View("index");
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {
            if (HttpContext.Session.GetInt32("Meals") <= 0)
            {
                TempData["Message"] = "You're out of Meals and cannot feed your pet";
            }
            else
            {
                int? MealsTemp = HttpContext.Session.GetInt32("Meals") - 1;
                HttpContext.Session.SetInt32("Meals", (int)MealsTemp);
                Random rand = new Random();
                // 25% chance pet won't like the meal.
                int chance = rand.Next(0, 4);
                if (chance == 0)
                {
                    TempData["Message"] = "Your pet didn't like the meal you fed it.";
                }
                else
                {
                    int FullAmount = rand.Next(5, 11);
                    int? FullnessTemp = HttpContext.Session.GetInt32("Fullness") + FullAmount;
                    HttpContext.Session.SetInt32("Fullness", (int)FullnessTemp);
                    TempData["Message"] = $"Your pet liked the meal you fed it. Your pet gained {FullAmount} Fullness!";

                }

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {
            if (HttpContext.Session.GetInt32("Energy") <= 0)
            {
                TempData["Message"] = "You are out of Energy and cannot play right now.";
            }
            Random rand = new Random();
            // 25% chance pet won't like the meal.
            int chance = rand.Next(0, 4);
            if (chance == 0)
            {
                TempData["Message"] = "Your pet didn't like the game you played with it.";
            }
            else
            {
                int HappinessAmount = rand.Next(5, 11);
                int? HappinessTemp = HttpContext.Session.GetInt32("Happiness") + HappinessAmount;
                HttpContext.Session.SetInt32("Happiness", (int)HappinessTemp);
                TempData["Message"] = $"You played with your pet and it gained {HappinessAmount} Happiness!";

            }
            int? EnergyTemp = HttpContext.Session.GetInt32("Energy") - 5;
            HttpContext.Session.SetInt32("Energy", (int)EnergyTemp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            if (HttpContext.Session.GetInt32("Energy") <= 0)
            {
                TempData["Message"] = "You are out of Energy and cannot work right now.";
            }
            Random rand = new Random();

            // Chance to earn 1-3 meals 
            int MealsAmount = rand.Next(1, 4);
            int? MealsTemp = HttpContext.Session.GetInt32("Meals") + MealsAmount;
            HttpContext.Session.SetInt32("Meals", (int)MealsTemp);
            TempData["Message"] = $"You worked hard, using 5 Energy, and you earned {MealsAmount} Meals!";

            // You lose 5 energy from working.
            int? EnergyTemp = HttpContext.Session.GetInt32("Energy") - 5;
            HttpContext.Session.SetInt32("Energy", (int)EnergyTemp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            if(HttpContext.Session.GetInt32("Fullness") <= 0)
            {
                TempData["Message"] = "Your Fullness is 0 and you cannot fall asleep.";
            }
            else if(HttpContext.Session.GetInt32("Happiness") <= 0)
            {
                TempData["Message"] = "Your Happiness is 0 and you cannot fall asleep.";
            }
            else if (HttpContext.Session.GetInt32("Fullness") <= 0 && HttpContext.Session.GetInt32("Happiness") <= 0)
            {
                TempData["Message"] = "Your Happiness AND Fullness are 0 and you cannot fall asleep.";
            }

            // Sleeping increases energy by 15 and decreases fullness and happiness by 5.
            int? EnergyTemp = HttpContext.Session.GetInt32("Energy") + 15;
            HttpContext.Session.SetInt32("Energy", (int)EnergyTemp);

            int? FullnessTemp = HttpContext.Session.GetInt32("Fullness") -5;
            HttpContext.Session.SetInt32("Fullness", (int)FullnessTemp);

            int? HappinessTemp = HttpContext.Session.GetInt32("Happiness") -5;
            HttpContext.Session.SetInt32("Happiness", (int)HappinessTemp);

            TempData["Message"] = "You gained 15 Energy and lost 5 Fullness and 5 Happiness.";
        
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}