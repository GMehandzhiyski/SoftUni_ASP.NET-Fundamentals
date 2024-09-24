namespace Watchlist.Models.Movie;

using System.ComponentModel.DataAnnotations;
using Watchlist.Models.Genre;
using static Common.ValidationConstants.Movie;

public class FormMovieViewModel
{
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
    public string Rating { get; set; } = null!;

    public int GenreId { get; set; } 

    public ICollection<GenreViewModel>? Genres { get; set; } 
 } 

