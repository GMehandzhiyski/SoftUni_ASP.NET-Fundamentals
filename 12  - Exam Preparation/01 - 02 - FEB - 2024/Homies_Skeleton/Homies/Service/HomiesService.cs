using Homies.Common;
using Homies.Contract;
using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;
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
