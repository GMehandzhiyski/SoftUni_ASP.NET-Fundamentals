using Homies.Contract;
using Homies.Extensions;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static Homies.Common.DateConstants;
using static Homies.Extensions.ClaimsPrincipalExtensions;

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

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            try
            {
                var model = new AddViewModel();
                model.Types = await data.GetTypesAsync();

                return View(model);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel viewModel)
        {

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            try
            {
                if (!ModelState.IsValid)
                {

                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    return View(viewModel);
                }

                bool IsTypeIdValid = await data.IsTypeValid(viewModel.TypeId);

                if (!IsTypeIdValid)
                {
                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    ModelState.AddModelError("Type", "The selected event type is invalid");
                    return View(viewModel);
                }


                DateTime.TryParseExact(
                       viewModel.Start,
                        DateFormatType,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out start);

                if (start == DateTime.MinValue)
                {
                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    ModelState.AddModelError(nameof(viewModel.Start), $"Invalid date! Format must be: {DateFormatType}");
                    viewModel.Start = string.Empty;
                    return View(viewModel);
                }

                DateTime.TryParseExact(
                    viewModel.End,
                    DateFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out end);

                if (end == DateTime.MinValue)
                {
                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    ModelState.AddModelError(nameof(viewModel.End), $"Invalid date! Format must be: {DateFormatType}");
                    viewModel.End = string.Empty;
                    return View(viewModel);
                }



                await data.AddAsync(viewModel, end, start, User.GetUserId());
                return RedirectToAction(nameof(All));
            }

            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            try
            {
                bool IsOrganiserIsOwner = await data.IsOrganiserEventOwnerAsync(id, User.GetUserId());

                if (!IsOrganiserIsOwner)
                {
                    return RedirectToAction(nameof(All));
                }

                var viewModel = await data.EditGetAsync(id);

                if (viewModel == null)
                {
                    return RedirectToAction(nameof(All));
                }

                viewModel.Types = await data.GetTypesAsync();
                return View(viewModel);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, AddViewModel viewModel)
        {
            try
            {
                bool IsOrganiserIsOwner = await data.IsOrganiserEventOwnerAsync(id, User.GetUserId());

                if (!IsOrganiserIsOwner)
                {
                    return RedirectToAction(nameof(All));
                }

                if (!ModelState.IsValid)
                {

                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    return View(viewModel);
                }

                bool IsTypeIdValid = await data.IsTypeValid(viewModel.TypeId);

                if (!IsTypeIdValid)
                {
                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    ModelState.AddModelError("Type", "The selected event type is invalid");
                    return View(viewModel);
                }

                DateTime startEdit;
                DateTime.TryParseExact(
                   viewModel.Start,
                   DateFormatType,
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out startEdit);

                if (startEdit == DateTime.MinValue)
                {
                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    ModelState.AddModelError(nameof(viewModel.Start), $"Invalid date! Format must be: {DateFormatType}");
                    viewModel.Start = string.Empty;
                    return View(viewModel);
                }

                DateTime endEdit;
                DateTime.TryParseExact(
                    viewModel.End,
                    DateFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out endEdit);

                if (endEdit == DateTime.MinValue)
                {
                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    ModelState.AddModelError(nameof(viewModel.End), $"Invalid date! Format must be: {DateFormatType}");
                    viewModel.End = string.Empty;
                    return View(viewModel);
                }

                viewModel.OrganiserId = User.GetUserId();

                await data.EditPostAsync(id, viewModel, startEdit, endEdit);
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
                var viewModel = await data.DetailsAsync(id);

                if (viewModel == null)
                {
                    return View("All", "Event");
                }

                return View(viewModel);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }


        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            try
            {
                bool isUserIsJoined = await data.IsUserAlreadyJoined(User.GetUserId(),id);

                if (isUserIsJoined)
                {
                    return RedirectToAction(nameof(Joined));
                }

                await data.JoinEvent(User.GetUserId(), id);
               
                return RedirectToAction(nameof(Joined));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            try
            {
                var allJoinedEvent = await data.AllJoinedEventsAsync(User.GetUserId());

                return View(allJoinedEvent);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            try
            {
                await data.LeaveEventAsync(id);

                return RedirectToAction(nameof(Joined));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
    

        }
    }
}
