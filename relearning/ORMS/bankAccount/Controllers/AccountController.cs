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
            User CurrentUser = _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("CurrentUserId")).Include(t => t.Transactions).SingleOrDefault();

            if (CurrentUser == null)
            {
                ViewBag.LoginError = "Sorry, a problem occured while attempting to grab your account. Please login again.";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.CurrentUser = CurrentUser;
            return View("Account");
        }

        [HttpPost]
        [Route("ChangeBalance")]
        public IActionResult ChangeBalance(string TransactionType, double TransactionAmount)
        {
            try
            {
                // Ensure TransactionAmount is a positive integer.
                if (TransactionAmount < 0.01 || TransactionAmount > int.MaxValue)
                {
                    TempData["TransactionError"] = "Please only use a valid, positive, integer.";
                    return RedirectToAction("Account");
                }

                // Check to ensure that the user has enough money in their account to cover the withdrawal and return error if not.
                if (TransactionType == "Withdrawal")
                {
                    User CurrentUser = _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("CurrentUserId")).Include(t => t.Transactions).SingleOrDefault();

                    // This computes the current balance for the user.
                    double CurrentUserBalance = 0;
                    foreach (var transaction in CurrentUser.Transactions)
                    {
                        if (transaction.Debits == 0)
                        {
                            CurrentUserBalance += transaction.Credits;
                        }
                        else
                        {
                            CurrentUserBalance -= transaction.Debits;
                        }
                    }

                    if (CurrentUserBalance < TransactionAmount)
                    {
                        TempData["TransactionError"] = "You cannot withdraw more money than you currently have in your account. Please try again.";
                        return RedirectToAction("Account");
                    }
                }

                // Separate transaction threads depending on Transaction type, Deposit vs Withdrawal.
                Transaction NewTransaction = new Transaction();
                int? CurrentUserId = HttpContext.Session.GetInt32("CurrentUserId");
                if (TransactionType == "Deposit")
                {
                    NewTransaction.UserId = (int)CurrentUserId;
                    NewTransaction.Debits = 0;
                    NewTransaction.Credits = TransactionAmount;
                }
                else
                {
                    {
                        NewTransaction.UserId = (int)CurrentUserId;
                        NewTransaction.Debits = TransactionAmount;
                        NewTransaction.Credits = 0;
                    };
                }
                // Send new Transaction to the DB.
                _context.Transactions.Add(NewTransaction);
                _context.SaveChanges();
            }
            // Catch will fire if there was an error creating or saving the new transaction in the DB>
            catch
            {
                TempData["TransactionError"] = "Sorry, There was a problem processing your transaction. Please check your account to see if the transaction processed and then try again.";
                return RedirectToAction("Account");
            }
            return RedirectToAction("Account");

        }
    }
}
