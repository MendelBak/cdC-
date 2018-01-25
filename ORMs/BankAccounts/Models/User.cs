using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BankAccounts.Models
{
    public abstract class BaseEntity { }

    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }

        // The "One" end of the One-To-Many relationship with transactions.
        public List<Transaction> Transactions { get; set; }
        // Constructor that ensures at least an empty List exists for this relationship to avoid NullExceptionErrors.
        public User()
        {
            Transactions = new List<Transaction>();
        }

        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Login : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
