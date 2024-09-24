namespace Homies.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Type = Models.Type;

public class TypeEntityConfiguration : IEntityTypeConfiguration<Type>
{
    public void Configure(EntityTypeBuilder<Type> builder)
    {
        ICollection<Type> types = GenerateTypes();

        builder.HasData(types);
    }

    private ICollection<Type> GenerateTypes()
    {
        ICollection<Type> types = new List<Type>()
        {
            new Type()
            {
                Id = 1,
                Name = "Animals"
            },
            new Type()
            {
                Id = 2,
                Name = "Fun"
            },
            new Type()
            {
                Id = 3,
                Name = "Discussion"
            },
            new Type()
            {
                Id = 4,
                Name = "Work"
            }
        };  

        return types;
    } 

}
