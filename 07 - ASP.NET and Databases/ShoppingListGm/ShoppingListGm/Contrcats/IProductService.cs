using ShoppingListGm.Models;

namespace ShoppingListGm.Contrcats
{
	public interface IProductService
	{
		Task<IEnumerable<ProductViewModel>> GetAllAsync();

		Task<ProductViewModel> GetByIdAsync(int id);

		Task AddProductAsync(ProductViewModel model);

		Task UpdateProductAsync(ProductViewModel model);

		Task DeleteProductAsync(ProductViewModel model);	

	}
}
