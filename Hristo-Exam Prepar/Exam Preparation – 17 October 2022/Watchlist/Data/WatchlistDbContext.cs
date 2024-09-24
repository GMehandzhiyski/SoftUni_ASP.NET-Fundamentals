using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Watchlist.Data.Models;

namespace Watchlist.Data
{
    public class WatchlistDbContext : IdentityDbContext
    {
        public WatchlistDbContext(DbContextOptions<WatchlistDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;

        public DbSet<Genre> Genres { get; set; } = null!;

        public DbSet<IdentityUserMovie> IdentitiyUsersMovies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
           /* builder
                .Entity<Genre>()
                .HasData(new Genre()
                {
                    Id = 1,
                    Name = "Action"
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Comedy"
                },
                new Genre()
                {
                    Id = 3,
                    Name = "Drama"
                },
                new Genre()
                {
                    Id = 4,
                    Name = "Horror"
                },
                new Genre()
                {
                    Id = 5,
                    Name = "Romantic"
                });
           */

            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(WatchlistDbContext)) ?? Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}