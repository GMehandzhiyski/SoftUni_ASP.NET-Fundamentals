﻿using GameZone.Contract;
using GameZone.Data;
using GameZone.Data.Models;
using GameZone.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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


        public async Task<IEnumerable<GameAllViewModel>> GetAllGamesAsync()
        {
            return await context.Games
                .AsNoTracking()
                .Select(g => new GameAllViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    ReleasedOn = g.ReleasedOn.ToString(DataFormatType),
                    Publisher = g.Publisher.UserName,
                    Genre = g.Genre.Name,
                })
                .ToListAsync();
        }
        public async Task AddAsync(GameAddFormModel model, DateTime releaseOn, string creatorId)
        { 
            Game newGame = new Game() 
            { 
              Title = model.Title,
              Description = model.Description,
              ImageUrl = model.ImageUrl,
              PublisherId = creatorId,
              ReleasedOn = releaseOn,
              GenreId = model.GenreId,
            };

            await context.Games.AddAsync(newGame);
            await context.SaveChangesAsync();

        }

        public async Task<bool> IsGenreIsValidAsync(int genreId)
        {
            return await context.Genres
                .AnyAsync(g => g.Id == genreId);
        }
        public async Task<IEnumerable<GenreViewModel>> GetGenresAsync()
        {
            return await context.Genres
                .AsNoTracking()
                .Select(g => new GenreViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToListAsync();
        }
    }
}
