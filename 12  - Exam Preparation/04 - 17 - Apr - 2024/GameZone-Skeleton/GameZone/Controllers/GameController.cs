using GameZone.Contract;
using GameZone.Extensions;
using GameZone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static GameZone.Common.DateConstants;

namespace GameZone.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService data;

        public GameController(IGameService _data)
        {
            data = _data;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                GameAddFormModel model = new GameAddFormModel();
                model.Genres = await data.GetGenresAsync();

                return View(model);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(GameAddFormModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var geners = await data.GetGenresAsync();
                    model.Genres = geners;  
                    return View(model); 
                }

                bool isGenreIsValid = await data.IsGenreIsValidAsync(model.GenreId);

                if (!isGenreIsValid)
                {
                    var geners = await data.GetGenresAsync();
                    model.Genres = geners;
                    ModelState.AddModelError("Genre", "The selected event type is invalid");
                    return View(model);
                }

                DateTime releasedOn = DateTime.UtcNow;
                DateTime.TryParseExact(
                    model.ReleasedOn,
                    DataFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out releasedOn);

                if (releasedOn == DateTime.MinValue)
                {
                    var geners = await data.GetGenresAsync();
                    model.Genres = geners;
                    ModelState.AddModelError(nameof(model.ReleasedOn), $"Invalid date! Format must be: {DataFormatType}");
                    model.ReleasedOn = string.Empty;

                    return View(model); 
                }

                await data.AddAsync(model, releasedOn, User.GetUserId());
                return RedirectToAction("All");

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var allGames = await data.GetAllGamesAsync();

                return View(allGames);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                bool isUserIsOwner = await data.IsUserIsOwner(id, User.GetUserId());

                if (isUserIsOwner == false)
                {
                    return RedirectToAction(nameof(All));
                }
                
               GameAddFormModel currGame = await data.GetGamesAsync(id);

                if (currGame == null)
                {
                    return RedirectToAction(nameof(All));
                }

                currGame.Genres = await data.GetGenresAsync();

                return View(currGame);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
