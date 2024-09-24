namespace Homies.Data.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static Common.ValidationConstants.Type;

public class Type
{
    public Type()
    {
        this.Events = new List<Event>();
    }

    [Key]
    [Comment("Type primary key ")]
    public int Id { get; set; }

    [Required]
    [Comment("Name of the type")]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = null!;

    [Comment("Collection of events")]
    public ICollection<Event> Events { get; set; }
}
