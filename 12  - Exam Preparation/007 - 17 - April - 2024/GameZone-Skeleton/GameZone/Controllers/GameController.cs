using GameZone.Data;
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
    }
}
