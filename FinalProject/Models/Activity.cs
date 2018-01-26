using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinalProject.Models
{

    public class Activity : BaseEntity
    {
        public int ActivityId { get; set; }
        public int AdminId { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }

        // This sets if the duration is measured in hours or minutes.
        public string DurationType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Many To Many relationship with User model. Many Users can sign up for many events.
        public List<Subscription> Guests { get; set; }
        public Activity()
        {
            Guests = new List<Subscription>();
        }
       
    }
}
