namespace Watchlist.Services;

using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Models.Genre;

public class GenreService : IGenreService
{
    private readonly WatchlistDbContext dbContext;

    public GenreService(WatchlistDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<ICollection<GenreViewModel>> GetAllAsync()
    {
        return await dbContext.Genres
                     .Select(g => new GenreViewModel()
                     {
                         Id = g.Id,
                         Name = g.Name,
                     })
                     .ToListAsync();
    }
}
