namespace Watchlist.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Common.ValidationConstants.Genre;

public class Genre
{
    public Genre()
    {
       this.Movies = new HashSet<Movie>(); 
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = null!;

    public ICollection<Movie> Movies { get; set;}
}
