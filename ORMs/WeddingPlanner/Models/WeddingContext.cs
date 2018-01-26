using Microsoft.EntityFrameworkCore;
using System;


namespace WeddingPlanner.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }
        
        // First variable should mirror the model class name. Second variable should mirror DB table name. (In PostgreSQL it will create schemas and tables that mirror these variables.) //
        public DbSet<User> Users { get; set; }
        public DbSet<Wedding> Weddings { get; set; }
    }

}
