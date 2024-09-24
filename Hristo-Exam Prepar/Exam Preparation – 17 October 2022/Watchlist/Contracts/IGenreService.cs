using Watchlist.Models.Genre;

namespace Watchlist.Contracts;

public interface IGenreService
{
    public Task<ICollection<GenreViewModel>> GetAllAsync();
}
