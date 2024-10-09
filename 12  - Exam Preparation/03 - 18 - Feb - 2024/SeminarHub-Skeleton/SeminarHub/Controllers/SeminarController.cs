using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeminarHub.Contract;
using SeminarHub.Extensions;
using SeminarHub.Models;
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


;            }
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

       
    }
}
