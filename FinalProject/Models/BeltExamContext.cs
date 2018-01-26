using System;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Models
{
    public class BeltExamContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BeltExamContext(DbContextOptions<BeltExamContext> options) : base(options) { }
        // First variable should mirror the model class name. Second variable should mirror DB table name. (In PostgreSQL it will create schemas and tables that mirror these variables.)
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }

}
