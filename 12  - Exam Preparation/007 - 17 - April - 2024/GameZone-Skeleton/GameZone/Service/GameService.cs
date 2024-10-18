using GameZone.Data;
using GameZone.Data.Models;
using GameZone.Models;
using GameZone.Service.Contract;
using Microsoft.EntityFrameworkCore;
using static GameZone.Common.DateConstants;

namespace GameZone.Service
{
    public class GameService : IGameService
    {
        private readonly GameZoneDbContext context;

        public GameService(GameZoneDbContext _context)
        {
            context = _context;
        }

        public async Task<GameAddFormModel?> GetGameEditAsync(int gameId)
        {
            return await context.Games
                .Where(g => g.IsDelete == false)
                .Where(g => g.Id == gameId)
                .Select(g => new GameAddFormModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    ImageUrl = g.ImageUrl,
                    PublisherId = g.PublisherId,
                    ReleasedOn = g.ReleasedOn.ToString(DateFormatType),
                    GenreId = g.GenreId,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<GameAllViewModel>> GetAllGamesAsync()
        {
            return await context.Games
                .Where(g => g.IsDelete == false)
                .Select(g => new GameAllViewModel()
                { 
                   Id = g.Id,
                   Title = g.Title,
                   ImageUrl = g.ImageUrl,
                   Publisher = g.Publisher.UserName,
                   ReleasedOn = g.ReleasedOn.ToString(DateFormatType),
                   Genre = g.Genre.Name
                })
                .ToListAsync();
        }

        public async Task AddGameAsync(GameAddFormModel model,DateTime releaseOn, string userId)
        {
            Game newGame = new Game()
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PublisherId = userId,
                ReleasedOn = releaseOn,
                GenreId = model.GenreId,
            };

            await context.Games.AddAsync(newGame);
            await context.SaveChangesAsync();
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

        public async Task<bool> IsGenreValid(int genreId)
        {
            return await context.Genres
                .AnyAsync(g => g.Id == genreId);
        }

        public async Task<bool> IsUserOwner(int gameId, string userId)
        {
            return await context.Games
                .Where(g => g.IsDelete == false)
                .AnyAsync(g => g.Id == gameId
                            && g.PublisherId == userId);
        }
    }
}
