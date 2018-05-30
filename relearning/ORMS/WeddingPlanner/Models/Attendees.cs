using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    // Join table for Many to Many relationship between Users and Weddings
    public class Atendees
    {
        public int AtendeesId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WeddingsId { get; set; }
        public Weddings Weddings { get; set;  }
    }
}