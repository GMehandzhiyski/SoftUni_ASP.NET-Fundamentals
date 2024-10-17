using GameZone.Models;

namespace GameZone.Service.Contract
{
    public interface IGameService
    {
        Task<ICollection<GameGenreViewModel>> GetGenresAsync();
    }
}
