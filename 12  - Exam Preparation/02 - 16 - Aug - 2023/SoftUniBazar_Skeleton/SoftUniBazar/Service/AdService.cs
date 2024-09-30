using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Contract;
using SoftUniBazar.Data;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;
using SoftUniBazar.Common;
using Homies.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SoftUniBazar.Data.Models;

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

        public async Task AddAsync(AdFormModel formModel, string creatorId)
        {
            Ad model = new Ad()
            {
                Name = formModel.Name,
                Description=formModel.Description,
                Price = formModel.Price,
                OwnerId = creatorId,
                ImageUrl=formModel.ImageUrl,
                CreatedOn = DateTime.Now,
                CategoryId = formModel.CategoryId
            };

            await context.Ads.AddAsync(model);
            await context.SaveChangesAsync();
        
        }

        public async Task<bool> isCategoryValid(int categoryId)
        {
            return await context.Categories
                .AnyAsync(c => c.Id == categoryId);
        }
    }

}
