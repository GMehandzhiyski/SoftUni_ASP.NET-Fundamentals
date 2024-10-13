using GameZone.Models;

namespace GameZone.Contract
{
    public interface IGameService
    {
        Task<IEnumerable<GenreViewModel>> GetGenresAsync();
        Task<bool> IsGenreIsValidAsync(int genreId);

        Task AddAsync(GameAddFormModel model, DateTime releaseOn, string creatorId);

        Task<IEnumerable<GameAllViewModel>> GetAllGamesAsync();
        Task<bool> IsUserIsOwner(int gameId, string userId);
        Task<GameAddFormModel> GetGamesAsync(int gameId);
        Task EditGameAsync(int id, GameAddFormModel model, DateTime releaseOn);
    }
}
