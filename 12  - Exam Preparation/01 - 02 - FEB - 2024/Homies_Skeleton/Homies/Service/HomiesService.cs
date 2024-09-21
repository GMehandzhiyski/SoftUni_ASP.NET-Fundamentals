using Homies.Contract;
using Homies.Data;

namespace Homies.Service
{
    public class HomiesService : IHomiesService
    {
        private readonly HomiesDbContext context;

        public HomiesService(HomiesDbContext _context)
        { 
            context = _context;
        }
    }
}
