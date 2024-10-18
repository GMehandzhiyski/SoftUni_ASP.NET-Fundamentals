using GameZone.Models;

namespace GameZone.Service.Contract
{
    public interface IGameService
    {
        Task<ICollection<GameGenreViewModel>> GetGenresAsync();

        Task<bool> IsGenreValid(int genreId);

        Task AddGameAsync(GameAddFormModel model, DateTime releaseOn, string userId);

        Task<ICollection<GameAllViewModel>> GetAllGamesAsync();

        Task<GameAddFormModel?> GetGameEditAsync(int gameId);

        Task<bool> IsUserOwner(int gameId, string userId);

        Task EditGameAsync(int gameId, GameAddFormModel model, DateTime releaseOn);

        Task<GameDetailsViewModel?> GetDetailsAsync(int gameId);
        Task<ICollection<GameMyZoneViewModel>> GetMyZoneGames(string userId);


    }
}
