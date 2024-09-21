using Homies.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homies.Configuration
{
    public class HomiesConfiguration : IEntityTypeConfiguration<Event>
    {
        private Event[] initialEvent = new Event[] 
        {
            new Event
        {
            Id = 2,
            Name = "Tech Conference 2024",
            Description = "A conference for technology enthusiasts and professionals.",
            OrganiserId = "30c121b4-41dd-4b57-9448-243bcf213958", // Updated ID
            CreatedOn = new DateTime(2024, 1, 10),
            Start = new DateTime(2024, 5, 12, 9, 0, 0),
            End = new DateTime(2024, 5, 12, 17, 0, 0),
            TypeId = 1
        },
        new Event
        {
            Id = 3,
            Name = "Music Festival 2024",
            Description = "A festival celebrating the best in music.",
            OrganiserId = "30c121b4-41dd-4b57-9448-243bcf213958", // Updated ID
            CreatedOn = new DateTime(2024, 2, 5),
            Start = new DateTime(2024, 6, 15, 14, 0, 0),
            End = new DateTime(2024, 6, 15, 22, 0, 0),
            TypeId = 2
        },
        new Event
        {
            Id = 4,
            Name = "Startup Pitch Event",
            Description = "An event for startups to pitch their ideas to investors.",
            OrganiserId = "30c121b4-41dd-4b57-9448-243bcf213958", // Updated ID
            CreatedOn = new DateTime(2024, 3, 20),
            Start = new DateTime(2024, 7, 20, 10, 0, 0),
            End = new DateTime(2024, 7, 20, 15, 0, 0),
            TypeId = 3
        }
        };

        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasData(initialEvent);
        }
    }
}
