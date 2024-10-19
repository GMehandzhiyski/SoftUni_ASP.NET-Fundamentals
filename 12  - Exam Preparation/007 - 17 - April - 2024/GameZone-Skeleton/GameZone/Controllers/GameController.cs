using GameZone.Data;
using GameZone.Extensions;
using GameZone.Models;
using GameZone.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Runtime.InteropServices;
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
                if (ModelState.IsValid == false)
                {
                    model.Genres = await data.GetGenresAsync();
                    return View(model);
                }

                bool isGenreValid = await data.IsGenreValid(model.GenreId);
                if (isGenreValid == false)
                {
                    model.Genres = await data.GetGenresAsync();
                    ModelState.AddModelError("Genre", "The selected event genre is invalid");
                    return View(model);
                }

                DateTime releaseOn = DateTime.Now;
                DateTime.TryParseExact(
                    model.ReleasedOn,
                    DateFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out releaseOn);

                if (releaseOn == DateTime.MinValue)
                {
                    model.Genres = await data.GetGenresAsync();
                    ModelState.AddModelError(nameof(model.ReleasedOn), $"Invalid date! Format must be: {DateFormatType}");
                    return View(model);
                }

                await data.AddGameAsync(model, releaseOn, User.GetUserId());

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
                ICollection<GameAllViewModel> allGames = await data.GetAllGamesAsync();

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
                bool isUserOwner = await data.IsUserOwner(id, User.GetUserId());
                if (isUserOwner == false)
                {
                    return RedirectToAction(nameof(All));
                }

                GameAddFormModel? model = await data.GetGameEditAsync(id);
                if (model == null)
                {
                    return RedirectToAction(nameof(All));
                }

                model.Genres = await data.GetGenresAsync();

                return View(model);
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
                    model.Genres = await data.GetGenresAsync();
                    return View(model);
                }

                bool isGenreValid = await data.IsGenreValid(model.GenreId);
                if (isGenreValid == false)
                {
                    model.Genres = await data.GetGenresAsync();
                    ModelState.AddModelError("Genre", "The selected event genre is invalid");
                    return View(model);
                }

                DateTime releaseOn = DateTime.Now;
                DateTime.TryParseExact(
                    model.ReleasedOn,
                    DateFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out releaseOn);

                if (releaseOn == DateTime.MinValue)
                {
                    model.Genres = await data.GetGenresAsync();
                    ModelState.AddModelError(nameof(model.ReleasedOn), $"Invalid date! Format must be: {DateFormatType}");
                    return View(model);
                }

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
                GameDetailsViewModel? model = await data.GetDetailsAsync(id);
                if (model == null)
                {
                    return RedirectToAction(nameof(All));
                }

                return View(model);
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
                ICollection<GameMyZoneViewModel> model = await data.GetMyZoneGames(User.GetUserId());

                return View(model);
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
                bool isUserHaveGame = await data.IsUserHaveGame(id, User.GetUserId());
                if (isUserHaveGame)
                {
                    return RedirectToAction(nameof(MyZone));
                }

                await data.AddGameToMyZone(id, User.GetUserId());

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
                bool isUserHaveGame = await data.IsUserHaveGame(id, User.GetUserId());
                if (isUserHaveGame == false)
                {
                    return RedirectToAction(nameof(MyZone));
                }

                await data.RemoveGameFromMyZone(id, User.GetUserId());

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
                GameDeleteViewModel? gameDelete = await data.GetGameForDelete(id);
                if (gameDelete == null)
                {
                    return RedirectToAction(nameof(All));
                }

                if (gameDelete.Publisher != User.GetUserName())
                {
                    return RedirectToAction(nameof(All));
                }

                return View(gameDelete);
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
                GameDeleteViewModel? gameDelete = await data.GetGameForDelete(id);
                if (gameDelete == null)
                {
                    return RedirectToAction(nameof(All));
                }

                if (gameDelete.Publisher != User.GetUserName())
                {
                    return RedirectToAction(nameof(All));
                }

                await data.DeleteGameAsync(gameDelete.Id);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }
    }
}
