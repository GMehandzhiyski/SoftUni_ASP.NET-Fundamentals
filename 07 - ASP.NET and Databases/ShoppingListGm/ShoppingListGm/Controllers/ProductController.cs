using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoppingListGm.Contrcats;
using ShoppingListGm.Data;

namespace ShoppingListGm.Controllers
{
	public class ProductController : Controller
	{
	
		private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var model = await productService.GetAllAsync();
			return View();
		}
	}
}
