using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bankAccount.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public double Debits { get; set; }
        public double Credits { get; set; }
    }
}