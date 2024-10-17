using GameZone.Data;
using GameZone.Service.Contract;
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
    }
}
