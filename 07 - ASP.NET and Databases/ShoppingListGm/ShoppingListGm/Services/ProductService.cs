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
                .AsNoTracking()
                .Select(p => new ProductViewModel()
				{ 
					Id = p.Id,
					ProductName = p.ProductName
				})
				.ToListAsync();
			
		}
		public async Task AddProductAsync(ProductViewModel model)
		{
			var entity = new Product()
			{
				ProductName = model.ProductName
			};

			await context.Products.AddAsync(entity);
			await context.SaveChangesAsync();
		}

		public async Task<ProductViewModel> GetByIdAsync(int id)
		{
			var returnProduct = await context
				.Products.FindAsync(id);

			if (returnProduct == null)
			{
				throw new ArgumentException("Invalid Product");
			}

			return new ProductViewModel()
			{
				Id = id,
				ProductName = returnProduct.ProductName
            };
			

        }

		public async Task UpdateProductAsync(ProductViewModel model)
		{
            var entity = await context.Products.FindAsync(model.Id);

            if (entity == null)
            {
                throw new ArgumentException("Invalid Product");
            }

            entity.ProductName = model.ProductName;
            await context.SaveChangesAsync();

        }

        public async Task DeleteProductAsync(int id)
		{
			var removeProduct = await context.Products.FindAsync(id);

			if (removeProduct == null)
			{
				throw new ArgumentException("Invalid Product");
			}

			context.Products.Remove(removeProduct);
			await context.SaveChangesAsync();
		}

	}
}
