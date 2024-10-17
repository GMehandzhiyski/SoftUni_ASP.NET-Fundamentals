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

        Task<SeminarDetailsViewModel?> GetDetailsAsync(int seminarId);

        Task<SeminarDeleteVIewModel?> GetSeminarForDeleting(int seminarId);

        Task DeleteSeminarAsync(int currSeminarId);

        Task<ICollection<SeminarJoinedViewModel>> GetAllJoinedModels(string userId);

        Task<bool> IsUserHaveSeminar(int seminarId, string userId);

        Task JoinUserToSeminar(int seminarId, string userId);

        Task LeaveSeminar(int seminarId, string userId);

    }
}
