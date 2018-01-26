using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{

    public class Wedding : BaseEntity
    {
        public int WeddingId { get; set; }
        public int AdminId { get; set;}
        public string WifeName { get; set; }
        public string HusbandName { get; set; }
        public String Address { get; set; }
        public DateTime DateOfWedding { get; set; }
        public DateTime CreatedAt { get; set; }

        // // The "One" end of the One-To-Many relationship with transactions.
        // public List<Transaction> Transactions { get; set; }
        // // Constructor that ensures at least an empty List exists for this relationship to avoid NullExceptionErrors.
        // public User()
        // {
        //     Transactions = new List<Transaction>();
        // }        
    }


}
