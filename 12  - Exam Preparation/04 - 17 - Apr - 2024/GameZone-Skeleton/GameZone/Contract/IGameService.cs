using GameZone.Models;

namespace GameZone.Contract
{
    public interface IGameService
    {
        Task<IEnumerable<GenreViewModel>> GetGenresAsync();
        Task<bool> IsGenreIsValidAsync(int genreId);

        Task AddAsync(GameAddFormModel model, DateTime releaseOn, string creatorId);

        Task<IEnumerable<GameAllViewModel>> GetAllGamesAsync();
    }
}
