using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeminarHub.Extensions;
using SeminarHub.Models;
using SeminarHub.Service.Contract;
using System.Globalization;
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
                var model = new SeminarAddFormModel();
                model.Categories = await data.GetCategoriesAsync();
                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarAddFormModel model)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    ICollection<SeminarCategoryViewModel> category = await data.GetCategoriesAsync();
                    model.Categories = category;
                    return View(model); 
                }

                bool isCategoryValid = await data.IsCategoryValid(model.CategoryId);

                if (isCategoryValid == false)
                {
                    ICollection<SeminarCategoryViewModel> category = await data.GetCategoriesAsync();
                    model.Categories = category;
                    ModelState.AddModelError("Category", "The selected event category is invalid");
                    return View(model);
                }

                DateTime dateAndTime = DateTime.Now;
                DateTime.TryParseExact(
                    model.DateAndTime,
                    DateFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out dateAndTime);

                if (dateAndTime == DateTime.MinValue)
                {
                    ICollection<SeminarCategoryViewModel> category = await data.GetCategoriesAsync();
                    model.Categories = category;
                    ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {DateFormatType}");
                    return View(model);
                }

                await data.AddSeminarAsync(model, dateAndTime, User.GetUserId());

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
                ICollection<SeminarAllViewModel> allSeminars = await data.GetAllSeminarsAsync();
                return View(allSeminars);
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
                bool isUserOwner = await data.IsUserOwnerAsync(id, User.GetUserId());

                if (isUserOwner == false)
                {
                     return RedirectToAction(nameof(All));
                }

                SeminarAddFormModel model = await data.GetSeminarAsync(id);

                if (model == null)
                {
                    return RedirectToAction(nameof(All));
                }

                model.Categories = await data.GetCategoriesAsync();

                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SeminarAddFormModel model)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    ICollection<SeminarCategoryViewModel> category = await data.GetCategoriesAsync();
                    model.Categories = category;
                    return View(model);
                }

                bool isCategoryValid = await data.IsCategoryValid(model.CategoryId);

                if (isCategoryValid == false)
                {
                    ICollection<SeminarCategoryViewModel> category = await data.GetCategoriesAsync();
                    model.Categories = category;
                    ModelState.AddModelError("Category", "The selected event category is invalid");
                    return View(model);
                }

                DateTime dateAndTime = DateTime.Now;
                DateTime.TryParseExact(
                    model.DateAndTime,
                    DateFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out dateAndTime);

                if (dateAndTime == DateTime.MinValue)
                {
                    ICollection<SeminarCategoryViewModel> category = await data.GetCategoriesAsync();
                    model.Categories = category;
                    ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {DateFormatType}");
                    return View(model);
                }

                await data.EditSeminarAsync(id, model, dateAndTime);

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
                SeminarDetailsViewModel? model = await data.GetDetailsAsync(id);

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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                SeminarDeleteVIewModel? currSeminar = await data.GetSeminarForDeleting(id);

                if (currSeminar == null)
                {
                    return RedirectToAction(nameof(All));
                }

                if (currSeminar.OrganizerId != User.GetUserId())
                {
                    return RedirectToAction(nameof(All));
                }

                return View(currSeminar);
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
                SeminarDeleteVIewModel? currSeminar = await data.GetSeminarForDeleting(id);

                if (currSeminar == null)
                {
                    return RedirectToAction(nameof(All));
                }

                if (currSeminar.OrganizerId != User.GetUserId())
                {
                    return RedirectToAction(nameof(All));
                }

                await data.DeleteSeminarAsync(currSeminar.Id);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            ICollection<SeminarJoinedViewModel> allModelsJoined = await data.GetAllJoinedModels(User.GetUserId());
           
            return View(allModelsJoined);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            try
            {
                bool isUserHaveSeminar = await data.IsUserHaveSeminar(id, User.GetUserId());

                if (isUserHaveSeminar)
                {
                    return RedirectToAction(nameof(Joined));
                }

                await data.JoinUserToSeminar(id, User.GetUserId());

                return RedirectToAction(nameof(Joined));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            bool isUserHaveSeminar = await data.IsUserHaveSeminar(id, User.GetUserId());

            if (!isUserHaveSeminar)
            {
                return RedirectToAction(nameof(Joined));
            }

            await data.LeaveSeminar(id, User.GetUserId());

            return RedirectToAction(nameof(Joined));
        }
    }
}
