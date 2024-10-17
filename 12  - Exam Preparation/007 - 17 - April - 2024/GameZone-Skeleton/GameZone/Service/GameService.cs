using GameZone.Data;
using GameZone.Models;
using GameZone.Service.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GameZone.Service
{
    public class GameService : IGameService
    {
        private readonly GameZoneDbContext context;

        public GameService(GameZoneDbContext _context)
        {
            context = _context;
        }

        public async Task<ICollection<GameGenreViewModel>>GetGenresAsync()
        {
            return await context.Genres
                .Select(g => new GameGenreViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,  
                })
                .ToListAsync();
        }
    }
}
