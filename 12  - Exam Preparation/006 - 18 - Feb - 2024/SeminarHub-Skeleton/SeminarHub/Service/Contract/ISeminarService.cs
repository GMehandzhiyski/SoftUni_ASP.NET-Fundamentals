using SeminarHub.Models;

namespace SeminarHub.Service.Contract
{
    public interface ISeminarService
    {
        Task<ICollection<SeminarCategoryViewModel>> GetCategoriesAsync();

        Task<bool> IsCategoryValid(int categoryId);

        Task AddSeminarAsync(SeminarAddFormModel model, DateTime dateAndTime, string creatorId);

        Task<bool> IsUserOwnerAsync(int seminarId, string organizerId);

        Task<SeminarAddFormModel> GetSeminarAsync(int seminarId);

        Task<ICollection<SeminarAllViewModel>> GetAllSeminarsAsync();

        Task EditSeminarAsync(int seminarId, SeminarAddFormModel model, DateTime dateAndTime);
    }
}
