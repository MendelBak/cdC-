using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using DbConnection;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index() 
        {
            List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM quotes_data");
            ViewBag.AllUsers = AllUsers;
            return View("index");
        }

        [HttpPost]
        [Route("submitQuote")]
        public IActionResult submitQuote(string name, string quote) {
            string Name = '"' + name + '"';
            string Quote = '"' + quote + '"';
            string NowTime = "NOW()";
             

            DbConnector.Execute($"INSERT INTO quotes_data (name, quote, created_at) VALUES ({Name}, {Quote}, {NowTime})");
            return RedirectToAction("Results");
        }

        [HttpGet]
        [Route("Results")]
        public IActionResult Results() 
        {
            ViewBag.quotes_data = DbConnector.Query($"SELECT * FROM quotes_data");
            return View("results");
        }

    }
}
