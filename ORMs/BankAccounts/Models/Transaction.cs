using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{

    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }
        public double Deposit { get; set; }
        public double Withdrawal { get; set; }

        // This saves the ID of the user who made a specific tranaction.
        public int UserId { get; set; }
        
        // This is the Foreign Key linking to the User model.
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
