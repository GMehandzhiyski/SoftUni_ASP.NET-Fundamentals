using GameZone.Data;
using GameZone.Models;
using GameZone.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
