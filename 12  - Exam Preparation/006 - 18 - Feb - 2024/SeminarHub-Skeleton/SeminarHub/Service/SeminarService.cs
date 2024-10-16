using SeminarHub.Contract;
using SeminarHub.Data;

namespace SeminarHub.Service
{
    public class SeminarService : ISeminarService
    {
        private readonly SeminarHubDbContext context;

        public SeminarService(SeminarHubDbContext _context)
        {
            context = _context;
        }



    }
}
