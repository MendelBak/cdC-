using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using bankAccount.Models;
using Microsoft.EntityFrameworkCore;


namespace bankAccount.Controllers
{
    public class AccountController : Controller
    {
        private BankContext _context;
    
        public AccountController(BankContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("Account")]
        public IActionResult Account()
        {
            User CurrentUser = _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("CurrentUserId")).SingleOrDefault();
            ViewBag.CurrentUser = CurrentUser;
            if(CurrentUser == null)
            {
                ViewBag.LoginError = "Sorry, a problem occured while attempting to grab your account. Please login again.";
                return View("Index", "Home");
            }
            return View("Account");
        }

        [HttpPost]
        [Route("ChangeBalance")]
        public IActionResult ChangeBalance(TransactionViewModel model)
        {
            if(ModelState.IsValid)
            {

            }
            return RedirectToAction("Account");
        }
    }
}
