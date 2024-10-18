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

        public async Task<ICollection<GameMyZoneViewModel>> GetMyZoneGames(string userId)
        { 
            return await context.GamerGames
                .Where(gg => gg.GamerId == userId)
                .Where(gg => gg.Game.IsDelete == false)
                .Select(gg => new GameMyZoneViewModel()
                {
                    Id = gg.Game.Id,
                    Title = gg.Game.Title,
                    ImageUrl = gg.Game.ImageUrl,
                    Publisher = gg.Game.Publisher.UserName,
                    ReleasedOn = gg.Game.ReleasedOn.ToString(DateFormatType),
                    Genre = gg.Game.Genre.Name
                })
                .ToListAsync();
        }

        public async Task<GameDetailsViewModel?> GetDetailsAsync(int gameId)
        {
            return await context.Games
                .Where(g => g.IsDelete == false)
                .Where(g => g.Id == gameId)
                .Select(g => new GameDetailsViewModel()
                {
                   Id = g.Id,
                   Title = g.Title,
                   Description  = g.Description,
                   ImageUrl = g.ImageUrl,
                   Publisher = g.Publisher.UserName,
                   ReleasedOn  = g.ReleasedOn.ToString(DateFormatType),
                   Genre = g.Genre.Name,
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditGameAsync(int gameId,GameAddFormModel model, DateTime releaseOn)
        {
            Game? cuurGame = await context.Games
                .Where(g => g.IsDelete == false)
                .Where(g => g.Id == gameId)
                .FirstOrDefaultAsync();

            if (cuurGame != null)
            {   
                cuurGame.Title = model.Title;
                cuurGame.Description = model.Description;   
                cuurGame.ImageUrl = model.ImageUrl;
                cuurGame.ReleasedOn = releaseOn;
                cuurGame.GenreId = model.GenreId;


                await context.SaveChangesAsync();
            }
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
