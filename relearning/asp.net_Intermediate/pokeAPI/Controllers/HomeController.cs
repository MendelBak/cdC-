using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using quotingDojo.Models;
using System.Net.Http;

namespace quotingDojo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("{pokeId}")]
        public IActionResult Index(int pokeId)
        {
            var PokeInfo = new Dictionary<string, object>();
            WebRequest.GetPokemonDataAsync(pokeId, ApiResponse =>
                {
                    PokeInfo = ApiResponse;
                }
            ).Wait();
            ViewBag.PokeInfo = PokeInfo;
            return View();
        }
    }
}
