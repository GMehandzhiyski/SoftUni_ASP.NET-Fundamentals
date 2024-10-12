using GameZone.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data
{
    public class GameZoneDbContext : IdentityDbContext
    {
        public GameZoneDbContext(DbContextOptions<GameZoneDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GamersGame>()
                .HasKey(gg => new { gg.GameId, gg.GamerId });

            builder.Entity<GamersGame>()
                .HasOne(g => g.Game)
                .WithMany(g => g.GamersGames)
                .HasForeignKey(g => g.GameId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GamersGame>()
                .HasOne(g => g.Gamer)
                .WithMany()
                .HasForeignKey(g => g.GamerId)
                .OnDelete(DeleteBehavior.NoAction);




            builder
                .Entity<Genre>()
                .HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Adventure" },
                new Genre { Id = 3, Name = "Fighting" },
                new Genre { Id = 4, Name = "Sports" },
                new Genre { Id = 5, Name = "Racing" },
                new Genre { Id = 6, Name = "Strategy" });

            base.OnModelCreating(builder);
        }

        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<GamersGame> GamersGames { get; set; } = null!;

        public DbSet<Genre> Genres { get; set; } = null!;   
    }
}
