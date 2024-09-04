using Microsoft.EntityFrameworkCore;
using ShoppingListGm.Contrcats;
using ShoppingListGm.Data;
using ShoppingListGm.Data.Models;
using ShoppingListGm.Models;

namespace ShoppingListGm.Services
{
	public class ProductService : IProductService
	{
		private readonly ShoppingListDbContext context;

        public ProductService(ShoppingListDbContext _context)
        {
            context = _context;	
        }


        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
		{
			return await context.Products
				.Select(p => new ProductViewModel
				{ 
					Id = p.Id,
					ProductName = p.ProductName
				})
				.AsNoTracking()
				.ToListAsync();
			
		}
		public Task<ProductViewModel> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task AddProductAsync(ProductViewModel model)
		{
			throw new NotImplementedException();
		}
		public Task UpdateProductAsync(ProductViewModel model)
		{
			throw new NotImplementedException();
		}

		public Task DeleteProductAsync(ProductViewModel model)
		{
			throw new NotImplementedException();
		}

	}
}
