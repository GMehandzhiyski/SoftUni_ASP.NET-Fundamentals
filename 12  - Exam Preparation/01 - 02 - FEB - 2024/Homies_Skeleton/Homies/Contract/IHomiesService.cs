using Homies.Models;

namespace Homies.Contract
{
    public interface IHomiesService
    {
        Task<IEnumerable<AllViewModel>> GetAllEventAsync();

        Task AddAsync(AddViewModel viewModel, DateTime end, DateTime start, string organiserID);

        Task<IEnumerable<TypeViewModel>> GetTypesAsync();

        Task<bool> IsTypeValid(int typeId);

        Task<bool> IsOrganiserEventOwnerAsync(int eventId, string userId);
        Task<AddViewModel?> EditGetAsync(int Id);

        Task EditPostAsync(int eventId, AddViewModel viewModel, DateTime start, DateTime end);
        Task<DetailsViewModel> DetailsAsync(int id);
        Task JoinEvent(string helperId, int eventId);
        Task<bool> IsUserAlreadyJoined(string userId, int eventId);
        Task<ICollection<AllViewModel>> AllJoinedEventsAsync(string userId);
        Task LeaveEventAsync(int eventId);



    }
}
