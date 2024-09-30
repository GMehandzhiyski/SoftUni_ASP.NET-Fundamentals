using Homies.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Contract;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;
using System.Xml.Linq;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly IAdService data;

        public AdController(IAdService _data)
        {
            data = _data;
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var viewModel = await data.GetAllAdAsync();

                return View(viewModel);
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
                AdFormModel viewModel = new AdFormModel();
                viewModel.Categories = await data.GetCategoriesAsync();
                
                return View(viewModel);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }


        }


        [HttpPost]
        public async Task<IActionResult> Add(AdFormModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var category = await data.GetCategoriesAsync();
                    model.Categories = category;
                    return View(model);
                }

                bool isCategoryValid = await data.isCategoryValid(model.CategoryId);

                if (!isCategoryValid)
                {
                    var category = await data.GetCategoriesAsync();
                    model.Categories = category;
                    ModelState.AddModelError("Category", "The selected event type is invalid");
                    return View(model);
                }


                await data.AddAsync(model, User.GetId());
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        
        }

    }
}
