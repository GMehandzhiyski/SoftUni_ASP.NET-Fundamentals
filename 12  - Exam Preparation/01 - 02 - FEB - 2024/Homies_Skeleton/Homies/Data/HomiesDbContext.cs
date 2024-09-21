using Homies.Configuration;
using Homies.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homies.Data
{
    public class HomiesDbContext : IdentityDbContext
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventParticipant>()
             .HasKey(ep => new { ep.EventId, ep.HelperId });

            modelBuilder.Entity<EventParticipant>()
                .HasOne(e => e.Event)
                .WithMany(e => e.EventParticipants)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Models.Type>()
                .HasData(new Models.Type()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new Models.Type()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new Models.Type()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new Models.Type()
                {
                    Id = 4,
                    Name = "Work"
                });

           modelBuilder.ApplyConfiguration(new HomiesConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Models.Type> Types { get; set; } = null!; 
        public DbSet<EventParticipant> EventParticipants { get; set; } = null!;

    }
}