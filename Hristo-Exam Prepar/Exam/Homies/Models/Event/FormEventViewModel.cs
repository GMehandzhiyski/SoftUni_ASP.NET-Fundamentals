namespace Homies.Models.Event;

using Homies.Models.Type;
using System.ComponentModel.DataAnnotations;

using static Common.ValidationConstants.Event;

public class FormEventViewModel
{
    public int Id { get; set; }

    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = null!;

    [MinLength(MinDescriptionLength)]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; } = null!;
     
    public string Start { get; set; } = null!;

    public string End { get; set; } = null!;

    public ICollection<TypeViewModel>? Types { get; set; } 

    public int TypeId { get; set; }

    public string? OrganiserId { get; set; }
}

