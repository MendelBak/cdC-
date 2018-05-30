using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public abstract class BaseEntity { }
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // One to Many relationship. One User can have many weddings.
        public List<Atendees> WeddingsAttending { get; set; }
        
        // Create an empty List to avoid null reference errors.
        public User()
        {
            WeddingsAttending = new List<Atendees>();
        }

    }
}