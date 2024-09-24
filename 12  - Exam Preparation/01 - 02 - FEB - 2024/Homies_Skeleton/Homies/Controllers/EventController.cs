﻿using Homies.Contract;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using static Homies.Common.DataConstants;
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
                        DateFormat,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out start);

                if (start == DateTime.MinValue)
                {
                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    ModelState.AddModelError(nameof(viewModel.Start), $"Invalid date! Format must be: {DateFormat}");
                    viewModel.Start = string.Empty;
                    return View(viewModel);
                }

                DateTime.TryParseExact(
                    viewModel.End,
                    DateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out end);

                if (end == DateTime.MinValue)
                {
                    var types = await data.GetTypesAsync();
                    viewModel.Types = types;
                    ModelState.AddModelError(nameof(viewModel.End), $"Invalid date! Format must be: {DateFormat}");
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

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                bool IsOrganiserIsOwner = await data.IsOrganiserEventOwnerAsync(id, User.GetUserId());

                if (!IsOrganiserIsOwner)
                {
                    return RedirectToAction(nameof(All));
                }

                var viewModel = await data.EditAsync(id);

                if (viewModel == null)
                {
                    return RedirectToAction(nameof(All));
                }

                return View(viewModel); 

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }        
        }


    }
}
