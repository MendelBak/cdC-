using Microsoft.EntityFrameworkCore;

namespace RESTauranter.Models
{
    public class ReviewContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ReviewContext(DbContextOptions<ReviewContext> options) : base(options) { }
        // First variable should mirror the model class name. Second variable should mirror DB table name.
        public DbSet<Review> Reviews { get; set; }
    }

}
