namespace Watchlist.Contracts;

using Watchlist.Models.Movie;
public interface IMovieService
{
    public Task<ICollection<AllMoviesViewModel>> GetAllAsync();

    public Task AddAsync(FormMovieViewModel model);

    public Task<bool> UserOwnsMovieAsync(string userId,int bookId);

    public Task AddMovieToUserCollectionAsync(string userId,int bookId);

    public Task RemoveMovieFromUserCollectionAync(string userId,int bookId);

    public Task<ICollection<WatchedMovieViewModel>> GetUserWatchedMoviesAsync(string userId);
}
