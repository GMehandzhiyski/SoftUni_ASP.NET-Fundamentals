using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SeminarHub.Contract;
using SeminarHub.Data.Models;
using SeminarHub.Extensions;
using SeminarHub.Models;
using System.Globalization;
using System.Runtime.InteropServices;
using static SeminarHub.Common.DateConstants;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly ISeminarService data;

        public SeminarController(ISeminarService _data)
        {
            data = _data;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            try
            {
                AddFormModel model = new AddFormModel();
                model.Categories = await data.GetCategoryAsync();

                return View(model);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFormModel model)
        {
            DateTime start = DateTime.Now;
            try
            {
                if (!ModelState.IsValid)
                {
                    var category = await data.GetCategoryAsync();
                    model.Categories = category;
                    return View(model);
                }

                bool isCategoryValid = await data.IsCategoryValidAsync(model.CategoryId);

                if (!isCategoryValid)
                {
                    var category = await data.GetCategoryAsync();
                    model.Categories = category;
                    ModelState.AddModelError("Category", "The selected event type is invalid");
                    return View(model);
                }


                DateTime.TryParseExact(
                    model.DateAndTime,
                    DateFormatConst,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out start);

                if (start == DateTime.MinValue)
                {
                    var category = await data.GetCategoryAsync();
                    model.Categories = category;
                    ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {DateFormatConst}");
                    model.DateAndTime = string.Empty;
                    return View(model);
                }

                await data.AddAsync(model, start, User.GetUserId());
                return RedirectToAction(nameof(Index));


                ; }
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
                var allSeminars = await data.GetAllSeminarsAsync();

                return View(allSeminars);
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
                bool isHaveSameSeminar = await data.IsHaveSeminar(id, User.GetUserId());

                if (isHaveSameSeminar)
                {
                    return RedirectToAction(nameof(Joined));
                }

                await data.JoinToCurrentSeminar(id, User.GetUserId());

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
                var allJoinedSeminar = await data.JoinedAsync(User.GetUserId());
                return View(allJoinedSeminar);
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
                await data.LeaveSeminarAsync(id, User.GetUserId());

                return RedirectToAction(nameof(Joined));
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
                bool isUserIsOwner = await data.IsUserIsOwnerAsync( id, User.GetUserId());

                if (!isUserIsOwner)
                {
                   return RedirectToAction(nameof(All)); 
                }

                var currModel = await data.EditGetSeminatAsync(id);

                if (currModel == null)
                {
                    return RedirectToAction(nameof(All));
                }

                currModel.Categories = await data.GetCategoryAsync();

                return View(currModel);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, AddFormModel model)
        {
            try
            {
                bool isUserIsOwner = await data.IsUserIsOwnerAsync(id, User.GetUserId());

                if (!isUserIsOwner)
                {
                    return RedirectToAction(nameof(All));   
                }

                if (!ModelState.IsValid)
                {
                    var category = await data.GetCategoryAsync();
                    model.Categories = category;
                    return View(model);

                }

                bool isCategoryValid = await data.IsCategoryValidAsync(model.CategoryId);

                if (!isCategoryValid)
                {
                    var category = await data.GetCategoryAsync();
                    model.Categories = category;
                    ModelState.AddModelError("Category", "The selected event type is invalid");
                    return View(model);
                }

                DateTime dataAndTime  = DateTime.Now;
                DateTime.TryParseExact(
                    model.DateAndTime,
                    DateFormatConst,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out dataAndTime);

                if (dataAndTime == DateTime.MinValue)
                {
                    var category = await data.GetCategoryAsync();
                    model.Categories = category;
                    ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {DateFormatConst}");
                    model.DateAndTime = string .Empty;  
                    return View(model);
                }

                model.OrganizerId = User.GetUserId();

                await data.EditSeminarAsync(id, model, dataAndTime);

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
                var model = await data.GetSeminarDetailsAsync(id);

                if (model == null)
                { 
                    return View(nameof(All));
                }

                return View(model);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var currSeminar = await data.FindSeminarAsync(id);

            if (currSeminar == null
                && currSeminar.OrganizerId != User.GetUserId())
            { 
                return RedirectToAction(nameof(All));
            }

            DeleteVIewModel model = new DeleteVIewModel()
            {
                Id = currSeminar.Id,
                Topic = currSeminar.Topic,
                DateAndTime = currSeminar.DateAndTime,
            };

            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var currSeminar = await data.FindSeminarAsync(id);

            if (currSeminar == null
                && currSeminar.OrganizerId != User.GetUserId())
            {
                return RedirectToAction(nameof(All));
            }

            await data.DeleteSeminarAsync(currSeminar);

            return RedirectToAction(nameof(All));
        }

    }
}
