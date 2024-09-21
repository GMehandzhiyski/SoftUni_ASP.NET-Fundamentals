using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Contract
{
    public interface IHomiesService
    {
        Task<IEnumerable<AllViewModel>> GetAllEventAsync();
    }
}
