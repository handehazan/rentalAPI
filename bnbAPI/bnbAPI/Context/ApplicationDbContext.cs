using bnbAPI.model;
using Microsoft.EntityFrameworkCore;

namespace bnbAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
             : base(options)
        {
        }

        public DbSet<Listing> Listings { get; set; }
        public DbSet<Stay> Stays { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
