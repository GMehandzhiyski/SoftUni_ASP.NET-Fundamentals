namespace Homies.Services;


using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using Homies.Contratcs;
using Homies.Data;
using Homies.Models.Type;

public class TypeService : ITypeService
{
    private readonly HomiesDbContext dbContext;

    public TypeService(HomiesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<TypeViewModel>> GetAllAsync()
    {
        return await dbContext.Types
                     .Select(t => new TypeViewModel()
                     {
                         Id = t.Id,
                         Name = t.Name,
                     })
                     .ToListAsync();
    }

    public async Task<bool> IsTypeValid(int typeId)
    {
        return await dbContext.Types
                     .AnyAsync(t => t.Id == typeId);
    }
}
