using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using BankAccounts.Models;

namespace BankAccounts.Controllers
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
        public IActionResult Account(int testID)
        {
            // Check to ensure there is a properly logged in user by checking session.
            if (HttpContext.Session.GetInt32("LoggedUserId") >= 0)
            {
                try
                {

                    // Save first name in session to display greeting on navbar.
                    ViewBag.FirstName = HttpContext.Session.GetString("LoggedUserName");
                    // Save id in session and then send to View using Viewbag
                    ViewBag.UserId = HttpContext.Session.GetInt32("LoggedUserId");
                    var AccountInfo = _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("LoggedUserId"));
                    ViewBag.AccountInfo = AccountInfo;
                    return View("Account");
                }
                // catch should only fire if there was an error getting/setting sesion id and username to ViewBag but if session id exists (which means a user is logged in). Send to page without greeting on navbar.
                catch
                {
                    return View("Account");
                }
            }
            // If no id is in session that means that the user is not properly logged on. Redirect to logout which will end up at LoginPage.
            return RedirectToAction("Logout");
        }


        [HttpPost]
        [Route("ChangeBalance")]
        public IActionResult ChangeBalance(string ChosenInputType, double Amount)
        {
            if (Amount < 0)
            {
                ViewBag.transactionError = "Sorry, you cannot enter a number less than zero(0). Please try again.";
                return View("Account");
            }
            else if(Amount > 0)
            {
                try
                {
                    // Get userID to save in transaction table.
                    int? LoggedUserId = HttpContext.Session.GetInt32("LoggedUserId");
                    // Check frontend radio button to determine if tramsaction is deposit or withdrawal.
                    if (ChosenInputType == "Deposit")
                    {
                        Transaction NewTransaction = new Transaction()
                        {
                            UserId = (int)LoggedUserId,
                            Deposit = Amount,
                            // Withdrawal = 0,
                            CreatedAt = DateTime.Now,
                        };
                        _context.Transactions.Add(NewTransaction);
                        _context.SaveChanges();
                    }
                    else if (ChosenInputType == "Withdrawal")
                    {
                        Transaction NewTransaction = new Transaction()
                        {
                            UserId = (int)LoggedUserId,
                            // Deposit = 0,
                            Withdrawal = Amount,
                            CreatedAt = DateTime.Now,
                        };
                        _context.Transactions.Add(NewTransaction);
                        _context.SaveChanges();
                    }
                }
                // Catch should only run if no user is logged in or if there is an error inserting the transaction into the DB.
                catch
                {
                    ViewBag.transactionError = "We apologize. There was an error processing your transaction. Please try again. If this error repeats please try logging out and back in. ";
                    return View("Account");
                }
                return RedirectToAction("Account");
            }
            return RedirectToAction("Account");
        }


        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            // LoginPage Method is in User Controller
            return RedirectToAction("LoginPage", "User");
        }






    }
}
