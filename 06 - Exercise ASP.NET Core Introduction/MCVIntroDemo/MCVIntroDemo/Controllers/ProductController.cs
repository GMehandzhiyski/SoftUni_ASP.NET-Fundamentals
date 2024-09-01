using MCVIntroDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
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

		public IActionResult AllAsText()
		{
			var text = string.Empty;
			foreach (var product in products)
			{
				text += $"Product {product.Id}: {product.Name} - {product.Price}.";
				text += "\r\n";
			}
			return Content(text);
		}

        public IActionResult AllAsTextFile()
        {
           StringBuilder sb = new StringBuilder();
            foreach (var product in products)
            {
				sb.AppendLine($"Product: {product.Id}: {product.Name} - {product.Price:f2} lv.");
            }

			Response.Headers.Add(HeaderNames.ContentDisposition,
				@"attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
        }
    }
}
