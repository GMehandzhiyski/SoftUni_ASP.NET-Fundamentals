namespace Watchlist.Data.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.ValidationConstants.Movie;

public class Movie
{
    public Movie()
    {
        this.UsersMovies = new HashSet<IdentityUserMovie>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(MinTitleLength)]
    [MaxLength(MaxTitleLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(MinDirectorNameLength)]
    [MaxLength(MaxDirectorNameLength)]
    public string Director { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [Range(MinRating,MaxRating)]
    [Precision(18,2)]
    public decimal Rating { get; set; }

    [Required]
    [ForeignKey(nameof(Genre))]
    public int GenreId { get; set; } 

    [Required]
    public Genre Genre { get; set; } = null!;

    public ICollection<IdentityUserMovie> UsersMovies { get; set; }
}
