﻿using Homies.Models;

namespace Homies.Contract
{
    public interface IHomiesService
    {
        Task<IEnumerable<EventViewModel>> GetAllEventAsync();

        Task AddAsync(EventAddViewModel viewModel, DateTime end, DateTime start, string organiserID);

        Task<IEnumerable<EventTypeViewModel>> GetTypesAsync();

        Task<bool> IsTypeValid(int typeId);

        Task<bool> IsOrganiserEventOwnerAsync(int eventId, string userId);
        Task<EventAddViewModel?> EditGetAsync(int Id);

        Task EditPostAsync(int eventId, EventAddViewModel viewModel, DateTime start, DateTime end);
        Task<EventDetailsViewModel> DetailsAsync(int id);
        Task JoinEvent(string helperId, int eventId);
        Task<bool> IsUserAlreadyJoined(string userId, int eventId);
        Task<ICollection<EventViewModel>> AllJoinedEventsAsync(string userId);
        Task LeaveEventAsync(int eventId);



    }
}
