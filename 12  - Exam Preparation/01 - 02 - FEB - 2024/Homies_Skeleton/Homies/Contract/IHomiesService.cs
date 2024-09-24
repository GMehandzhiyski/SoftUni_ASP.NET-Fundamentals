using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Homies.Contract
{
    public interface IHomiesService
    {
        Task<IEnumerable<AllViewModel>> GetAllEventAsync();

        Task AddAsync(AddViewModel viewModel, DateTime end, DateTime start, string organiserID);

        Task<IEnumerable<TypeViewModel>> GetTypesAsync();

        Task<bool> IsTypeValid(int typeId);
        Task<AddViewModel?> EditAsync(int Id);
        Task<bool> IsOrganiserEventOwnerAsync(int eventId, string userId);
    }
}
