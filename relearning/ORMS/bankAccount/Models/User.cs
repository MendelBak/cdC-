using System;
using System.Collections.Generic;

namespace bankAccount.Models
{
    public abstract class BaseEntity { }
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // One to Many Relationship. One User can have many transactions which will populate in this List when .Include() is called (similar to a SQL Join statement).
        public List<Transaction> Transactions { get; set; }

        // This creates an empty List so that we don't occur NullReferenceErrors.
        public User()
        {
            Transactions = new List<Transaction>();
        }

    }
}