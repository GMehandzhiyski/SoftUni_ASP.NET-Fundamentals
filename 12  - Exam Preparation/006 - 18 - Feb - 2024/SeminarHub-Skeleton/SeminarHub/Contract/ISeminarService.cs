using SeminarHub.Models;

namespace SeminarHub.Contract
{
    public interface ISeminarService 
    {
        Task<IEnumerable<SeminarCategoryViewModel>> GetCategoryAsync();

        Task<bool> IsCategoryIsValid(int categoryId);

        Task AddSeminarAsync(SeminarAddFormModel model, DateTime dateAndTime, string creatorId);

        Task<bool> IsUserIsOwnerAsync(int seminarId, string organizerId);

        Task<SeminarAddFormModel> GetSeminarAsync(int seminarId);

        Task<IEnumerable<SeminarAllViewModel>> GetAllSeminarsAsync();

        Task EditSeminarAsync(int seminarId, SeminarAddFormModel model, DateTime dateAndTime);
    }
}
