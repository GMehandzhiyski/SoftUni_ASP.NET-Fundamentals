using Homies.Contract;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using static Homies.Common.DataConstants;

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

        [HttpGet]

        public async Task<IActionResult> All()
        {
            var allEnevts = await data.GetAllEventAsync();

            return View(allEnevts);
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddViewModel viewModel)
        {

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            //if (!ModelState.IsValid)
            //{
            //    viewModel.Types = await GetTypes();

            //    return View(model);
            //}

            if (!DateTime.TryParseExact(
                viewModel.Start,
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(viewModel.Start), $"Invalid date! Format must be: {DateFormat}");
            }

            if (!DateTime.TryParseExact(
                viewModel.End,
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(nameof(viewModel.Start), $"Invalid date! Format must be: {DateFormat}");
            }



            await data.AddAsync(viewModel, end, start, GetUserId());
            return View(viewModel);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }


    }
}
