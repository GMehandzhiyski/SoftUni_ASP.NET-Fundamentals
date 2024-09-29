using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Contract;
using SoftUniBazar.Data;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;
using SoftUniBazar.Common;
using Homies.Common;

namespace SoftUniBazar.Service
{
    public class AdService : IAdService
    {
        private readonly BazarDbContext context;

        public AdService(BazarDbContext _context)
        {
            context = _context;
        }



        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            return await context.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel()
                { 
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        
        }

        public async Task<IEnumerable<AdViewModel>> GetAllAdAsync()
        {
            return await context.Ads
                .AsNoTracking()
                .Select(a => new AdViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImageUrl = a.ImageUrl,
                    CreatedOn = a.CreatedOn.ToString(DateTimeParseFormats.DefaultTimeFormat),
                    Category = a.Category.Name,
                    Description = a.Description,
                    Price = a.Price,
                    Owner = a.Owner.UserName
                })
                .ToListAsync();

        }

    }

}
