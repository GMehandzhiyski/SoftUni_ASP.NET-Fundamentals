using Microsoft.EntityFrameworkCore.ChangeTracking;
using SeminarHub.Contract;
using SeminarHub.Data;

namespace SeminarHub.Service
{
    public class SeminarService : ISeminarService
    {
        private readonly SeminarHubDbContext context;

        public SeminarService(SeminarHubDbContext _contex)
        {
            context = _contex;
        }
    }
}
