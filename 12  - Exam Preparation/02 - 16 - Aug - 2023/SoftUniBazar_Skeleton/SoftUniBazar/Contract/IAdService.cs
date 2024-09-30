using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;

namespace SoftUniBazar.Contract
{
    public interface IAdService
    {

        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();
        Task<IEnumerable<AdViewModel>> GetAllAdAsync();

        Task AddAsync(AdFormModel formModel, string creatorId);
        Task<bool> isCategoryValid(int categoryId);
    }
}
