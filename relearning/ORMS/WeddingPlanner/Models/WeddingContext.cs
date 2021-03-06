using Microsoft.EntityFrameworkCore;
 
namespace WeddingPlanner.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Weddings> Weddings { get; set; }
        public DbSet<Atendees> Atendees { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atendees>().HasKey(c => new { c.UserId, c.WeddingsId});
        }
    }

}
