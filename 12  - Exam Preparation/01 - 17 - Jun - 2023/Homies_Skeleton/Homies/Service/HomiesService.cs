using Homies.Common;
using Homies.Contract;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.EntityFrameworkCore;

namespace Homies.Service
{
    public class HomiesService : IHomiesService
    {
        private readonly HomiesDbContext context;

        public HomiesService(HomiesDbContext _context)
        { 
            context = _context;
        }

        public async Task LeaveEventAsync(int eventId)
        {
            EventParticipant? removeEvent = await context.EventParticipants
                .Where(ep => ep.EventId == eventId)
                .FirstOrDefaultAsync();
           
            if (removeEvent != null)
            {
                context.EventParticipants.Remove(removeEvent);
                await context.SaveChangesAsync();

            }

        }

        public async Task<ICollection<EventViewModel>> AllJoinedEventsAsync(string userId)
        {
            return await context.EventParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(e => new EventViewModel()
                {
                    Id = e.Event.Id,
                    Name = e.Event.Name,
                    Start = e.Event.Start.ToString(DateConstants.DateFormatType),
                    Type = e.Event.Type.Name,
                    Organiser = e.Event.OrganiserId,

                })
                .ToListAsync();
        
        }

        public async Task JoinEvent(string helperId, int eventId)
        {
            var participant = new EventParticipant()
            { 
                HelperId = helperId,
                EventId = eventId
            };

            await context.EventParticipants.AddAsync(participant);
            await context.SaveChangesAsync();
        }
        public async Task<EventDetailsViewModel> DetailsAsync(int id)
        { 
            return await context.Events
                .Where(e => e.Id == id)
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start.ToString(DateConstants.DateFormatType),
                    End = e.End.ToString(DateConstants.DateFormatType),
                    Organiser = e.Organiser.UserName,
                    CreatedOn = e.CreatedOn.ToString(DateConstants.DateFormatType),
                    Type = e.Type.Name,
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditPostAsync(int eventId, AddViewModel viewModel, DateTime start, DateTime end)
        { 
            Event currentModel = await context.Events
                .Where(e => e.Id == eventId)
                .FirstOrDefaultAsync();

            if (currentModel != null)
            {
                currentModel.Name = viewModel.Name;
                currentModel.Description = viewModel.Description;
                currentModel.Start = start;
                currentModel.End = end;
                currentModel.TypeId = viewModel.TypeId;
                currentModel.OrganiserId = viewModel.OrganiserId;

                await context.SaveChangesAsync();   
            }

        }

        public async Task<AddViewModel?> EditGetAsync(int Id)
        {
            return await context.Events
                .Where(e => e.Id == Id)
                .Select(e => new AddViewModel()
                { 
                    Name = e.Name,
                    Description = e.Description,    
                    Start = e.Start.ToString(DateConstants.DateFormatType),
                    End = e.End.ToString(DateConstants.DateFormatType),
                    TypeId = e.TypeId,  
                })
                .FirstOrDefaultAsync();
                

        }

        public async Task AddAsync(AddViewModel viewModel, DateTime end, DateTime start, string organiserID )
        {

            Event newEvent = new Event()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                OrganiserId = organiserID,
                CreatedOn = DateTime.Now,
                Start = start,
                End = end,
                TypeId = viewModel.TypeId
            };

            await context.Events.AddAsync(newEvent);
            await context.SaveChangesAsync();

        }


        public async Task<IEnumerable<EventViewModel>> GetAllEventAsync()
        {
            return await context.Events
                 .Select(e => new EventViewModel
                 {  
                     Id = e.Id, 
                     Name = e.Name, 
                     Start = e.Start.ToString(DateConstants.DateFormatType),
                     Type = e.Type.Name,
                     Organiser = e.Organiser.UserName,

                 })
                 .AsNoTracking()
                 .ToListAsync();     
        }

        public async Task<IEnumerable<EventTypeViewModel>> GetTypesAsync()
        {
            return await context.Types
                .AsNoTracking()
                .Select(t => new EventTypeViewModel 
                { 
                    Id = t.Id,
                    Name = t.Name,  
                } )
                .ToListAsync();
        }

        public async Task<bool> IsTypeValid(int  typeId)
        {
            return await context.Types
                .AnyAsync(t => t.Id == typeId);
        }

        public async Task<bool> IsOrganiserEventOwnerAsync(int eventId, string userId)
        {
            return await context.Events
                .AnyAsync(e => e.Id == eventId
                               && e.OrganiserId == userId);

        }

        public async Task<bool> IsUserAlreadyJoined(string userId, int eventId)
        {
            return await context.EventParticipants
                .AnyAsync(ep => ep.HelperId == userId
                              && ep.EventId == eventId);
        
        }
    }
}
