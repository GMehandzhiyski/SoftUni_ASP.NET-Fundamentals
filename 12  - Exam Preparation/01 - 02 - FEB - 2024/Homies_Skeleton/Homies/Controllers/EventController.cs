using Homies.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {

        private readonly IHomiesService data;

        public EventController(IHomiesService _data)
        {
            data = _data;
        }

        //[HttpGet]


    }
}
