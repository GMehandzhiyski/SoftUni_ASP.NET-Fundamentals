namespace Watchlist.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Xml.Linq;
using Watchlist.Data.Models;

public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        ICollection<Genre> genres = GenerateGenres();

        builder
            .HasData(genres);
    }

    private ICollection<Genre> GenerateGenres()
    {
        ICollection<Genre> genres = new List<Genre>()
        {
               new Genre()
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
                }
        };

        return genres;
    }
}
