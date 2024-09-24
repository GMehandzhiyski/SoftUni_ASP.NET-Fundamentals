namespace Library.Models.Book;
using System.ComponentModel.DataAnnotations;
using Library.Models.Category;
using static Common.ValidationConstants.Book;

public class BookFormViewModel
{
    public BookFormViewModel()
    {
        Categories = new List<CategoryViewModel>();
    }


    [Required]
    [MinLength(MinTitleLength)]
    [MaxLength(MaxTitleLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(MinAuthorNameLength)]
    [MaxLength(MaxAuthorNameLength)]
    public string Author { get; set; } = null!;

    [Required]
    [MinLength(MinDescriptionLength)]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Url { get; set; } = null!;

    [Required]
    public string Rating { get; set; } = null!;

    public int CategoryId { get; set; }

    public ICollection<CategoryViewModel> Categories { get; set; }

}
