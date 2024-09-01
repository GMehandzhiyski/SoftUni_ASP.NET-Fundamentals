using MCVIntroDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MCVIntroDemo.Controllers
{
	public class ProductController : Controller
	{

		private IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
		{
			new ProductViewModel()
			{ 
				Id = 1,
				Name = "Cheese",
				Price = 7.00
			},
			new ProductViewModel()
			{
				Id= 2,
				Name = "Ham",
				Price = 5.50
			},
			new ProductViewModel()
			{ 
				Id = 3,
				Name = "Bread",
				Price = 1.50
			},
		};

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult All()
		{
			return View(products);
		}
        public IActionResult ById(int id)
        {
			var filtredProduct = products
				.FirstOrDefault(x => x.Id == id);

			if (filtredProduct == null)
			{
				return BadRequest();
			}
            return View(filtredProduct);
        }

        public IActionResult AllAsJson()
        {
			var options = new JsonSerializerOptions
			{
				WriteIndented = true,
			};
			return Json(products, options);
        }
    }
}
