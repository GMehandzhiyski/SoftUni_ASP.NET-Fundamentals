using GameZone.Extensions;
using GameZone.Models;
using GameZone.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

                return RedirectToAction(nameof(All));

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

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, GameAddFormModel model)
        { 
            try
            {
                if (ModelState.IsValid == false)
                {
                    var genre = await data.GetGenresAsync();
                    model.Genres = genre;
                    return View(model);
                }

                bool isUserIsOwner = await data.IsUserIsOwner(id, User.GetUserId());

                if (isUserIsOwner == false)
                {
                    return RedirectToAction(nameof(All));
                }

                bool isGenreIsValid = await data.IsGenreIsValidAsync(model.GenreId);

                if (isGenreIsValid == false)
                {
                    var genre = await data.GetGenresAsync();
                    model.Genres = genre;   
                    ModelState.AddModelError("Genre", "The selected event type is invalid");
                    return View(model);
                }

                DateTime releaseOn = DateTime.Now;
                DateTime.TryParseExact(
                    model.ReleasedOn,
                    DataFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out releaseOn);

                if (releaseOn == DateTime.MinValue)
                {
                    var genre = await data.GetGenresAsync();
                    model.Genres = genre;   
                    ModelState.AddModelError(nameof(model.ReleasedOn), $"Invalid date! Format must be: {DataFormatType}");
                    return View(model);
                }

                model.PublisherId = User.GetUserId();

                await data.EditGameAsync(id, model, releaseOn);

                return RedirectToAction(nameof(All));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var currGame = await data.GetDetailsGame(id);

                if (currGame == null)
                {
                    return RedirectToAction(nameof(All));
                }

                return View(currGame);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyZone()
        {
            try
            {
                var allGameInMyZone = await data.GetAllInMyZone(User.GetUserId());

                return View(allGameInMyZone);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddToMyZone(int id)
        {
            try
            {
                bool isUserHaveSameGame = await data.IsUserHaveSameGame(id, User.GetUserId());

                if (isUserHaveSameGame)
                {
                    return RedirectToAction(nameof(All));
                }

                await data.JoinUserToGame(id, User.GetUserId());

                return RedirectToAction(nameof(MyZone));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> StrikeOut(int id)
        {
            try
            {
                bool isUserHaveSameGame = await data.IsUserHaveSameGame(id, User.GetUserId());

                if (isUserHaveSameGame == false)
                {
                    return RedirectToAction(nameof(All));
                }

                await data.LeaveThisGame(id, User.GetUserId());

                return RedirectToAction(nameof(MyZone));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var currGame = await data.GetGameByIdAsync(id);

                if (currGame == null
                    && currGame.PublisherId != User.GetUserId())
                {
                    return RedirectToAction(nameof(All));
                }

                GameDeleteViewModel model = new GameDeleteViewModel()
                {
                    Id = currGame.Id,
                    Title = currGame.Title,
                    Publisher = User.GetUserName()
                };

                return View(model);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var currGame = await data.GetGameByIdAsync(id);

                if (currGame == null
                    && currGame.PublisherId != User.GetUserId())
                {
                    return RedirectToAction(nameof(All));
                }

                await data.DeleteGameAsync(currGame);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }
    }
}
