using Microsoft.EntityFrameworkCore;

namespace RESTraunter.Models
{
    public class RestaurantContext :DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {  }

        public DbSet<Reviews> Reviews { get; set; }
    }
}