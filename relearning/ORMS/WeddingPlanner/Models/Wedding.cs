using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Weddings
    {
        public int WeddingsId { get; set; }
        public int AdminId { get; set; }
        public string Bride { get; set; }
        public string Groom { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public int NumGuests { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Atendees> Atendees { get; set; }
        public Weddings()
        {
            Atendees = new List<Atendees>();
        }
        
    }
}