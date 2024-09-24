namespace Homies.Contratcs;

using Homies.Models.Event;

public interface IEventService
{
    public Task<ICollection<AllEventsViewModel>> GetAllAsync();

    public Task CreateAsync(string organiserId, FormEventViewModel viewModel);

    public Task<FormEventViewModel?> GetFormModelByIdAsync(int id);

    public Task<bool> IsOrganiserEventOwnerAsync(int eventId, string userId);
     
    public Task EditAsync(int  eventId, FormEventViewModel viewModel);

    public Task<ICollection<AllJoinedEventsViewModel>> GetAllUserJoinedEventsAsync(string userId);

    public Task<bool> IsUserAlreadyParticipatingAsync(string userId, int eventId);

    public Task AddEventToUserAsync(string userId, int eventId);

    public Task RemoveEventFromUserAsync(string userId, int eventId);

    public Task<EventDetailsViewModel?> GetEventDetailsAsync(int eventId);
}
