﻿using GameZone.Data.Models;
using GameZone.Models;

namespace GameZone.Service.Contract
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

        Task<GameDetailsViewModel> GetDetailsGame(int id);

        Task<IEnumerable<GameMyZoneViewModel>> GetAllInMyZone(string ownerId);
        Task<bool> IsUserHaveSameGame(int gameId, string userId);

        Task JoinUserToGame(int gameId, string userId);
        Task LeaveThisGame(int gameId, string userId);
        Task<Game> GetGameByIdAsync(int gameId);
        Task DeleteGameAsync(Game currGame);
    }
}
