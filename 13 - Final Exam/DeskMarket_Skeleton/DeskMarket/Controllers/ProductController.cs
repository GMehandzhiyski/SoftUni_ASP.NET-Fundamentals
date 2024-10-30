using DeskMarket.Extensions;
using DeskMarket.Models;
using DeskMarket.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static DeskMarket.Common.DateConstants;

namespace DeskMarket.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IDeskService data;

        public ProductController(IDeskService _data)
        {
            data = _data;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<DeskIndexViewModel> model = new List<DeskIndexViewModel>();

                if (User.GetUserId() != null)
                {
                    model = await data.GetAllProductsAuthenticateAsync(User.GetUserId());
                }
                else
                {
                    model = await data.GetAllProductsUnAuthenticateAsync();
                }

                return View(model);
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
                DeskAddFormModel model = new DeskAddFormModel();
                model.Categories = await data.GetCategoryAsync();

                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(DeskAddFormModel model)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    model.Categories = await data.GetCategoryAsync();
                    return View(model);

                }

                bool isCategoryValid = await data.IsCategoryValid(model.CategoryId);
                if (isCategoryValid == false)
                {
                    model.Categories = await data.GetCategoryAsync();
                    ModelState.AddModelError("Category", "The selected event category is invalid");
                    return View(model);
                }

                DateTime addedOn = DateTime.Now;
                DateTime.TryParseExact(
                    model.AddedOn,
                    DateFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out addedOn);
                if (addedOn == DateTime.MinValue)
                {
                    model.Categories = await data.GetCategoryAsync();
                    ModelState.AddModelError(nameof(model.AddedOn), $"Invalid date! Format must be: {DateFormatType}");
                    return View(model);
                }

                await data.AddProductAsync(model, addedOn, User.GetUserId());

                return RedirectToAction(nameof(Index));

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
                DeskDetailsViewModel? model = await data.GetDetailsAsync(id, User.GetUserName(), User.GetUserId());
                if (model == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(model);
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
                bool isUserOwner = await data.IsUserOwner(id, User.GetUserId());
                if (isUserOwner == false)
                {
                    return RedirectToAction(nameof(Index));
                }

                DeskAddFormModel? model = await data.GetProductEditAsync(id);
                if (model == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                model.Categories = await data.GetCategoryAsync();

                return View(model);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DeskAddFormModel model)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    model.Categories = await data.GetCategoryAsync();
                    return View(model);

                }

                bool isCategoryValid = await data.IsCategoryValid(model.CategoryId);
                if (isCategoryValid == false)
                {
                    model.Categories = await data.GetCategoryAsync();
                    ModelState.AddModelError("Category", "The selected event category is invalid");
                    return View(model);
                }

                DateTime addedOn = DateTime.Now;
                DateTime.TryParseExact(
                    model.AddedOn,
                    DateFormatType,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out addedOn);
                if (addedOn == DateTime.MinValue)
                {
                    model.Categories = await data.GetCategoryAsync();
                    ModelState.AddModelError(nameof(model.AddedOn), $"Invalid date! Format must be: {DateFormatType}");
                    return View(model);
                }

                await data.EditProductAsync(id, model, addedOn);

                return RedirectToAction(nameof(Details));
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
                DeskDeleteViewModel? productDelete = await data.GetProductForDelete(id);
                if (productDelete == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (productDelete.SellerId != User.GetUserId())
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(productDelete);
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
                DeskDeleteViewModel? productDelete = await data.GetProductForDelete(id);
                if (productDelete == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (productDelete.SellerId != User.GetUserId())
                {
                    return RedirectToAction(nameof(Index));
                }

                await data.DeleteProductAsync(productDelete.Id);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            try
            {
                ICollection<DeskCartViewModel> model = await data.GetCartAsync(User.GetUserId());

                return View(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            try
            {

                bool isUserHaveProduct = await data.IsUserHaveProduct(id, User.GetUserId());
                if (isUserHaveProduct)
                {
                    return RedirectToAction(nameof(Index));
                }

                await data.AddProductToCart(id, User.GetUserId());

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            try
            {
                bool isUserHaveProduct = await data.IsUserHaveProduct(id, User.GetUserId());
                if (isUserHaveProduct == false)
                {
                    return RedirectToAction(nameof(Index));
                }

                await data.RemoveProductFromCart(id, User.GetUserId());

                return RedirectToAction(nameof(Cart));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }
    }
}
