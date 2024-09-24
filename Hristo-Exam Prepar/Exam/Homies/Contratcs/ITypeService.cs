namespace Homies.Contratcs;

using Homies.Models.Type;

public interface ITypeService
{
    public Task<ICollection<TypeViewModel>> GetAllAsync();

    public Task<bool> IsTypeValid(int typeId);
}
