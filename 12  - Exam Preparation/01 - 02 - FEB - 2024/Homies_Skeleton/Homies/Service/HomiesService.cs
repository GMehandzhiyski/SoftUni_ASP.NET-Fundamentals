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


        public async Task<AddViewModel?> EditAsync(int Id)
        {
            return await context.Events
                .Where(e => e.Id == Id)
                .Select(e => new AddViewModel()
                { 
                    Name = e.Name,
                    Description = e.Description,    
                    Start = e.Start.ToString(DataConstants.DateFormat),
                    End = e.End.ToString(DataConstants.DateFormat),
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


        public async Task<IEnumerable<AllViewModel>> GetAllEventAsync()
        {
            return await context.Events
                 .Select(e => new AllViewModel
                 {  
                     Id = e.Id, 
                     Name = e.Name, 
                     Start = e.Start.ToString(DataConstants.DateFormat),
                     Type = e.Type.Name,

                 })
                 .AsNoTracking()
                 .ToListAsync();     
        }

        public async Task<IEnumerable<TypeViewModel>> GetTypesAsync()
        {
            return await context.Types
                .AsNoTracking()
                .Select(t => new TypeViewModel 
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
    }
}
