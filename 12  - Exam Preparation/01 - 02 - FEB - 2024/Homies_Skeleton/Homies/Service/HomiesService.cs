using Homies.Common;
using Homies.Contract;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.EntityFrameworkCore;


using System.Security.Claims;

namespace Homies.Service
{
    public class HomiesService : IHomiesService
    {
        private readonly HomiesDbContext context;

        public HomiesService(HomiesDbContext _context)
        { 
            context = _context;
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

     
      
    }
}
