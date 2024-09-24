using Library.Contracts;
using Library.Data;
using Library.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class CategoryService : ICategoryService
{
    private readonly LibraryDbContext dbContext;

    public CategoryService(LibraryDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<CategoryViewModel>> GetAllAsync()
    {
        return await dbContext.Categories
                     .Select(c => new CategoryViewModel
                     {
                         Name = c.Name,
                         Id = c.Id,
                     })
                     .ToListAsync();
    }
}
