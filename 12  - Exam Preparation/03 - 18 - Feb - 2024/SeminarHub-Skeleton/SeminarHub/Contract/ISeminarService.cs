using SeminarHub.Models;

namespace SeminarHub.Contract
{
    public interface ISeminarService
    {
        Task<IEnumerable<CategoryViewModel>> GetCategoryAsync();

        Task<bool> IsCategoryValidAsync(int CategoryId);

        Task AddAsync(AddFormModel model, DateTime dateAndTime, string organizerId);

        Task<IEnumerable<AllViewModel>> GetAllSeminarsAsync();
    }
}
