using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;
using AjaxPagination.Models;

namespace AjaxPagination.Controllers
{

    public class HomeController : Controller
    {
        private UserContext _context;

        public HomeController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(string FirstName)
        {
            // Do a math.ceiling
            int TotalUsers = (_context.Users.Count() / 3);
            List<User> AllUsers = _context.Users.ToList();
            ViewBag.TotalUsers = TotalUsers;
            ViewBag.AllUsers = AllUsers;
            return View();
        }

        [HttpPost]
        [Route("FilterUsers")]
        public IActionResult FilterUsers(string LastName)
        {
            // Do a math.ceiling
            int TotalUsers = (_context.Users.Count() / 3);
            List<User> AllUsers = _context.Users.Where(e => e.last_name == LastName).ToList();
            ViewBag.TotalUsers = TotalUsers;
            ViewBag.AllUsers = AllUsers;
            return View("Index");
        }

        // This route takes the id of the last user that is being displayed on the view as well as the page (via pagination of all the users) that the user wants to go to and returns the approriate reponse.
        [HttpGet]
        [Route("ChoosePage/{lastId}/{selection}")]
        public IActionResult ChoosePage(int lastId, int selection)
        {
            System.Console.WriteLine("FOUND THE CORRECT SELECTION! HERE IT IS ->" + lastId + selection);
            return RedirectToAction("Index");
        }

        
    }
}
