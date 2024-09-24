namespace Watchlist.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Watchlist.Data.Models;

public class IdentityUserMovieConfiguration : IEntityTypeConfiguration<IdentityUserMovie>
{
    public void Configure(EntityTypeBuilder<IdentityUserMovie> builder)
    {
        builder.
               HasKey(b => new { b.MovieId, b.IdentityUserId });
    }
}
