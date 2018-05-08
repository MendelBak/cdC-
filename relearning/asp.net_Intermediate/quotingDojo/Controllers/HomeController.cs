using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DbConnection;

namespace quotingDojo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.submitted = false;
            return View();
        }

        [HttpPost]
        [Route("NewQuote")]
        public IActionResult NewQuote(string author, string quote)
        {
            ViewBag.submitted = true;
            if(author == null || quote == null)
            {
                TempData["Message"] = "You cannot leave the Quote or the Author fields empty.";
                return View("Index");
            }

            string SqlQuote = "'" + quote.Trim() + "'";
            string SqlAuthor = "'" + author.Trim() + "'";
            DbConnector.Execute($"INSERT INTO quotes (quote, author, created_at, updated_at) VALUES ({SqlQuote}, {SqlAuthor}, NOW(), NOW())");
            TempData["Message"] = "Thank you for your submission!";
            return View("Index");
        }

        [HttpGet]
        [Route("AllQuotes")]
        public IActionResult AllQuotes()
        {
            // TempData["allQuotes"] =
            ViewBag.AllQuotes = DbConnector.Query("SELECT * FROM quotes");
            return View();
        }

    }
}
