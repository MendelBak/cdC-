using System;
using System.ComponentModel.DataAnnotations;

namespace bankAccount.Models
{
    public class TransactionViewModel
    {
        [Required]
        [Range(0, int.MaxValue)]  
        public int UserId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage="Please enter a valid integer.")]
        public double Debits { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage="Please enter a valid integer.")]
        public double Credits { get; set; }     

        // This value is not saved into the DB but is only validated here.
        [Required]
        [Range(0, int.MaxValue, ErrorMessage="Please enter a valid integer.")]
        public double TransactionAmount { get; set; }
    }
}