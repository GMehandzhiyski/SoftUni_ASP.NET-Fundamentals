namespace Watchlist.Services;

using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Models.Movie;
using static Watchlist.Extensions.FormattingMethods;


public class MovieService : IMovieService
{
    private readonly WatchlistDbContext dbContext;

    public MovieService(WatchlistDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(FormMovieViewModel model)
    {
        Movie movie = new Movie()
        {
            Title = model.Title,
            Director = model.Director,
            Rating = DecimalToGlobalStandard(model.Rating),
            GenreId = model.GenreId,
            ImageUrl = model.ImageUrl,
        };

        await dbContext.Movies.AddAsync(movie);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddMovieToUserCollectionAsync(string userId, int bookId)
    {
        IdentityUserMovie movieToAdd = new IdentityUserMovie()
        {
            IdentityUserId = userId,
            MovieId = bookId
        };

        await dbContext.IdentitiyUsersMovies
              .AddAsync(movieToAdd);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<AllMoviesViewModel>> GetAllAsync()
    {
        return await dbContext.Movies
                     .AsNoTracking()
                     .Select(m => new AllMoviesViewModel
                     {
                         Id = m.Id,
                         Title = m.Title,
                         Director = m.Director,
                         Genre = m.Genre.Name,
                         Rating = m.Rating.ToString(),
                         ImageUrl = m.ImageUrl,
                     })
                     .ToListAsync();
    }

    public async Task<ICollection<WatchedMovieViewModel>> GetUserWatchedMoviesAsync(string userId)
    {
        return await dbContext.IdentitiyUsersMovies
                     .Where(u => u.IdentityUserId == userId)
                     .Select(m => new WatchedMovieViewModel
                     {
                         Id = m.Movie.Id,
                         Director = m.Movie.Director,
                         Genre = m.Movie.Genre.Name,
                         Rating = m.Movie.Rating.ToString(),
                         ImageUrl = m.Movie.ImageUrl,
                         Title = m.Movie.Title
                     })
                     .ToListAsync();
                     
                                 
    }
                     

    public async Task RemoveMovieFromUserCollectionAync(string userId, int bookId)
    {
        IdentityUserMovie? bookToRemove = await dbContext.IdentitiyUsersMovies
                                               .Where(u => u.IdentityUserId == userId && u.MovieId == bookId)
                                               .FirstOrDefaultAsync();

        if(bookToRemove != null)
        {
            dbContext.IdentitiyUsersMovies.Remove(bookToRemove);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> UserOwnsMovieAsync(string userId, int bookId)
    {

        return await dbContext.IdentitiyUsersMovies
                     .Where(u => u.IdentityUserId == userId && u.MovieId == bookId)
                     .AnyAsync();
    }
}
