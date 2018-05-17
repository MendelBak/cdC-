using System;
using System.ComponentModel.DataAnnotations;

namespace bankAccount.Models
{
    public class TransactionViewModel
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage="Please enter a valid integer.")]  
        public int TransactionAmount { get; set; }

        [Required]
        public bool TransactionType { get; set; }
    }
}