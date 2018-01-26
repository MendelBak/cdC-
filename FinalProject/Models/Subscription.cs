using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinalProject.Models
{

    public class Subscription : BaseEntity
    {
        // Many To Many relationship with User and Activity models. Many Users can sign up for many activities.
        public int SubscriptionId { get; set; }

        
        public int GuestId { get; set; }
        public User User { get; set; }


        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
