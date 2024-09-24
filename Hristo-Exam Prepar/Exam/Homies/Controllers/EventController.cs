namespace Homies.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

using Homies.Contratcs;
using Homies.Extensions;
using Homies.Models.Event;
using Homies.Models.Type;

using static Common.DateTimeParseFormats;

[Authorize]
public class EventController : Controller
{
    private readonly IEventService eventService;
    private readonly ITypeService typeService;

    public EventController(IEventService eventService, ITypeService typeService)
    {
        this.eventService = eventService;
        this.typeService = typeService;

    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {
            ICollection<AllEventsViewModel> events = await eventService.GetAllAsync();

            return View(events);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
        
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        try
        {
            FormEventViewModel model = new FormEventViewModel();
            ICollection<TypeViewModel> types = await typeService.GetAllAsync();

            model.Types = types;

            return View(model);

        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(FormEventViewModel inputModel)
    {
        try
        {
            bool isTypeValid = await typeService.IsTypeValid(inputModel.TypeId);

            if (!isTypeValid)
            {
                ICollection<TypeViewModel> types = await typeService.GetAllAsync();
                inputModel.Types = types;
                ModelState.AddModelError("Type", "The selected event type is invalid");
                return View(inputModel);
            }


            if(!ModelState.IsValid)
            {
                ICollection<TypeViewModel> types = await typeService.GetAllAsync();
                inputModel.Types = types;
                return View(inputModel);
            }

            DateTime startTime;
            DateTime.TryParseExact(inputModel.Start, DefaultTimeFormat, null, DateTimeStyles.None, out startTime);


            if (startTime == DateTime.MinValue)
            {
                ICollection<TypeViewModel> types = await typeService.GetAllAsync();
                inputModel.Types = types;
                inputModel.Start = String.Empty;
                ModelState.AddModelError("Start", "The selected start date is invalid. Please follow a time format as shown above in the field.");
                return View(inputModel);
            }

            DateTime endTime;
            DateTime.TryParseExact(inputModel.End, DefaultTimeFormat, null, DateTimeStyles.None, out endTime);

            if (endTime == DateTime.MinValue)
            {
                ICollection<TypeViewModel> types = await typeService.GetAllAsync();
                inputModel.Types = types;
                inputModel.End = String.Empty;
                ModelState.AddModelError("End", "The selected end date is invalid. Please follow a time format as shown above in the field.");
                return View(inputModel);
            }

            await eventService.CreateAsync(User.GetId(), inputModel);

            return RedirectToAction("All", "Event");

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
            bool isOrganiserEventOwner = await eventService.IsOrganiserEventOwnerAsync(id, User.GetId());

            if (!isOrganiserEventOwner)
            {
                return RedirectToAction("All", "Event");
            }
            FormEventViewModel? model = await eventService.GetFormModelByIdAsync(id);

            if(model == null) 
            {
                return RedirectToAction("All", "Event");
            }
            ICollection<TypeViewModel> types = await typeService.GetAllAsync();
            model.Types = types;
            return View(model);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, FormEventViewModel inputModel)
    {
        try
        {
            bool isOrganiserEventOwner = await eventService.IsOrganiserEventOwnerAsync(id, User.GetId());
            if (!isOrganiserEventOwner)
            {
                return RedirectToAction("All", "Event");
            }

            bool isTypeValid = await typeService.IsTypeValid(inputModel.TypeId);

            if (!isTypeValid)
            {
                ICollection<TypeViewModel> types = await typeService.GetAllAsync();
                inputModel.Types = types;
                ModelState.AddModelError("Type", "The selected event type is invalid");
                return View(inputModel);
            }


            if (!ModelState.IsValid)
            {
                ICollection<TypeViewModel> types = await typeService.GetAllAsync();
                inputModel.Types = types;
                return View(inputModel);
            }

            DateTime startTime;
            DateTime.TryParseExact(inputModel.Start, DefaultTimeFormat, null, DateTimeStyles.None, out startTime);


            if (startTime == DateTime.MinValue)
            {
                ICollection<TypeViewModel> types = await typeService.GetAllAsync();
                inputModel.Types = types;
                inputModel.Start = DateTime.Now.ToString(DefaultTimeFormat);
                ModelState.AddModelError("Start", "The selected start date is invalid. Please follow a time format as shown above in the field.");
                return View(inputModel);
            }

            DateTime endTime;
            DateTime.TryParseExact(inputModel.End, DefaultTimeFormat, null, DateTimeStyles.None, out endTime);

            if (endTime == DateTime.MinValue)
            {
                ICollection<TypeViewModel> types = await typeService.GetAllAsync();
                inputModel.Types = types;
                inputModel.End = DateTime.Now.ToString(DefaultTimeFormat);
                ModelState.AddModelError("End", "The selected end date is invalid. Please follow a time format as shown above in the field.");
                return View(inputModel);
            }

            inputModel.OrganiserId = User.GetId();

            await eventService.EditAsync(id, inputModel);

            return RedirectToAction("All", "Event");
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }

    public async Task<IActionResult> Joined()
    {
        try
        {
            ICollection<AllJoinedEventsViewModel> events = await eventService.GetAllUserJoinedEventsAsync(User.GetId());

            return View(events);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }

    public async Task<IActionResult> Join(int id)
    {
        try
        {
            bool isUserParcitipating = await eventService.IsUserAlreadyParticipatingAsync(User.GetId(), id);

            if(isUserParcitipating)
            {
                return RedirectToAction("All", "Event");
            }

            await eventService.AddEventToUserAsync(User.GetId(), id);

            return RedirectToAction("Joined", "Event");

        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            EventDetailsViewModel? model = await eventService.GetEventDetailsAsync(id);



            if (model == null)
            {
                return RedirectToAction("All", "Event");
            }

            return View(model);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }


    public async Task<IActionResult> Leave(int id)
    {
        try
        {
            await eventService.RemoveEventFromUserAsync(User.GetId(), id);
            return RedirectToAction("All", "Event");
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }

    


}
