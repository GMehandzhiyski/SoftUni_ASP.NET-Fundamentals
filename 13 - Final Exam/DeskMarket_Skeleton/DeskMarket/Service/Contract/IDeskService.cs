using DeskMarket.Models;
using System.Threading.Tasks;

namespace DeskMarket.Service.Contract
{
    public interface IDeskService 
    {
        Task<ICollection<DeskIndexViewModel>> GetAllProductsAuthenticateAsync(string userId);

        Task<ICollection<DeskIndexViewModel>> GetAllProductsUnAuthenticateAsync();

        Task<ICollection<DeskCategoryViewModel>> GetCategoryAsync();

        Task<bool> IsCategoryValid(int categoryId);

        Task AddProductAsync(DeskAddFormModel model, DateTime addedOn, string userId);

        Task<bool> IsUserOwner(int productId, string userId);

        Task<DeskDetailsViewModel?> GetDetailsAsync(int productId, string userName, string userId);

        Task<DeskAddFormModel?> GetProductEditAsync(int productId);

        Task EditProductAsync(int productId, DeskAddFormModel model, DateTime addedOn);

        Task<DeskDeleteViewModel?> GetProductForDelete(int productId);

        Task DeleteProductAsync(int productId);

        Task<ICollection<DeskCartViewModel>> GetCartAsync(string userId);

        Task<bool> IsUserHaveProduct(int productId, string userId);

        Task AddProductToCart(int productId, string userId);

        Task RemoveProductFromCart(int productId, string userId);

    }
}
