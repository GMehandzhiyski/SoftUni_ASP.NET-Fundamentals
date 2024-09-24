namespace Homies.Services;

using Microsoft.EntityFrameworkCore;

using Homies.Contratcs;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models.Event;

using static Common.DateTimeParseFormats;
using static Extensions.FormattingMethods;


public class EventService : IEventService
{
    private readonly HomiesDbContext dbContext;

    public EventService(HomiesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<AllEventsViewModel>> GetAllAsync()
    {
        return await dbContext.Events
                     .AsNoTracking()
                     .Select(e => new AllEventsViewModel()
                     {
                         Id = e.Id,
                         Name = e.Name,
                         Organiser = e.Organiser.UserName,
                         Type = e.Type.Name,
                         Start = FormatDateTime(e.Start)
                     })
                     .ToListAsync();
    }

    public async Task CreateAsync(string organiserId, FormEventViewModel viewModel)
    {
        Event eventToAdd = new Event()
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Start = DateTime.ParseExact(viewModel.Start, DefaultTimeFormat, null),
            End = DateTime.ParseExact(viewModel.End, DefaultTimeFormat, null),
            TypeId = viewModel.TypeId,
            OrganiserId = organiserId
        };

        await dbContext.Events.AddAsync(eventToAdd);
        await dbContext.SaveChangesAsync();
    }

    public async Task<FormEventViewModel?> GetFormModelByIdAsync(int id)
    {
        return await dbContext.Events
              .Where(e => e.Id == id)
              .Select(e => new FormEventViewModel()
              {
                  Id = e.Id,
                  Name = e.Name,
                  Description = e.Description,
                  Start = e.Start.ToString(DefaultTimeFormat),
                  End = e.End.ToString(DefaultTimeFormat),
                  TypeId=e.TypeId
              })
              .FirstOrDefaultAsync();
              

    }

    public async Task<bool> IsOrganiserEventOwnerAsync(int eventId, string userId)
    {
        return await dbContext.Events
                     .AnyAsync(e => e.OrganiserId == userId && e.Id == eventId);
    }

    public async Task EditAsync(int eventId, FormEventViewModel viewModel)
    {
        Event? eventToEdit = await dbContext.Events
                            .Where(e => e.Id == eventId)
                            .FirstOrDefaultAsync();

        if (eventToEdit != null)
        {
            eventToEdit.Name = viewModel.Name;
            eventToEdit.Start = DateTime.ParseExact(viewModel.Start, DefaultTimeFormat, null);
            eventToEdit.End = DateTime.ParseExact(viewModel.End, DefaultTimeFormat, null);
            eventToEdit.Description = viewModel.Description;
            eventToEdit.OrganiserId = viewModel.OrganiserId;
            eventToEdit.TypeId = viewModel.TypeId;

            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<ICollection<AllJoinedEventsViewModel>> GetAllUserJoinedEventsAsync(string userId)
    {
        return await dbContext.EventParticipants
                     .Where(e => e.HelperId == userId)
                     .Select(e => new AllJoinedEventsViewModel()
                     {
                         Id = e.Event.Id,
                         Name = e.Event.Name,
                         Start = e.Event.Start.ToString(DefaultTimeFormat),
                         Type = e.Event.Type.Name,
                         Organiser = e.Event.OrganiserId
                     })
                     .ToListAsync();
    }

    public async Task<bool> IsUserAlreadyParticipatingAsync(string userId, int eventId)
    {
        return await dbContext.EventParticipants
                     .AnyAsync(e => e.HelperId == userId && e.EventId == eventId);
    }

    public async Task AddEventToUserAsync(string userId, int eventId)
    {
        EventParticipant newEventParticipant = new EventParticipant()
        {
            EventId = eventId,
            HelperId = userId
        };

        await dbContext.EventParticipants.AddAsync(newEventParticipant);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveEventFromUserAsync(string userId, int eventId)
    {
        EventParticipant? eventParticipantToRemove = await dbContext.EventParticipants
                                                     .Where(ep => ep.EventId == eventId && ep.HelperId == userId)
                                                     .FirstOrDefaultAsync();

        if(eventParticipantToRemove != null)
        {
            dbContext.EventParticipants.Remove(eventParticipantToRemove);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<EventDetailsViewModel?> GetEventDetailsAsync(int eventId)
    {
        return await dbContext.Events
                     .Where(e => e.Id == eventId)
                     .Select(e => new EventDetailsViewModel()
                     {
                         Id = e.Id,
                         Name = e.Name,
                         Description = e.Description,
                         Start = e.Start.ToString(DefaultTimeFormat),
                         End = e.End.ToString(DefaultTimeFormat),
                         Organiser = e.Organiser.UserName,
                         CreatedOn = e.CreatedOn.ToString(DefaultTimeFormat),
                         Type = e.Type.Name

                     })
                     .FirstOrDefaultAsync();
    }
}
