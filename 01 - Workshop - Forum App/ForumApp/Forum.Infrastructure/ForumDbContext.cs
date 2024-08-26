using Forum.Infrastructure.Data.Configuration;
using Forum.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            :base(options)
        {

        }

        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());

            base.OnModelCreating(modelBuilder); 
        }
    }
}
