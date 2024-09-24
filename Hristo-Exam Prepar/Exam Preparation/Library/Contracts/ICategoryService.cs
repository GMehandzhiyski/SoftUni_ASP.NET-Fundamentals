using Library.Models.Category;

namespace Library.Contracts;

public interface ICategoryService
{
    public Task<ICollection<CategoryViewModel>> GetAllAsync();
}
