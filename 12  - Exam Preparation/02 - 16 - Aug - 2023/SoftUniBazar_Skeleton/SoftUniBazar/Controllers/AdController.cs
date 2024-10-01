using Homies.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
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
        public async Task<IActionResult> AllAsync()
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
        public async Task<IActionResult> AddAsync()
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
        public async Task<IActionResult> AddAsync(AdFormModel model)
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


                await data.AddAsync(model, User.GetUserId());
                return RedirectToAction(nameof(AllAsync));
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
                bool IsOwenerIsOwner = await data.IsOwnerAdOwenAsync(id, User.GetUserId());
                if (!IsOwenerIsOwner)
                {
                    return RedirectToAction("Ad","All");
                }

               AdFormModel model = await data.EditGetAsync(id);

                if (model == null)
                {
                    return RedirectToAction("Ad", "All");
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
        public async Task<IActionResult> EditAsync(AdFormModel model, int id)
        {
            try
            {
                await data.EditPostAsync(model, id);
                return RedirectToAction("Ad", "All");
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
