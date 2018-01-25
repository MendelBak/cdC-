using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccounts.Models
{
    public class AccountViewModel : BaseEntity
    {
        [Required]
        public int UserId { get; set; }


        // Backend validation will ensure that no empty transactions are submitted.//
        public double Deposit { get; set; }


        public double Withdrawal { get; set; }
    }
}