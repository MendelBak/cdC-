using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinalProject.Models
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

        
        // Many To Many relationship with User model. Many Users can sign up for many events.
        public List<Subscription> EventsAttending { get; set; }
        public User()
        {
            EventsAttending = new List<Subscription>();
        }



    }

    public class Login : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
