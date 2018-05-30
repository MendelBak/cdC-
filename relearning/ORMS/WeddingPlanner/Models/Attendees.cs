using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    // Join table for Many to Many relationship between Users and Weddings
    // Table ID is created as a composite primary key in the context file
    public class Atendees
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int WeddingsId { get; set; }
        public Weddings Weddings { get; set;  }
    }
}