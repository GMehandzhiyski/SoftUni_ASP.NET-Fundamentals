using System.ComponentModel.DataAnnotations;

namespace Forum.Core.Models.Post
{
    using static Forum.Common.Validation.ValidationConstants.Post;
    public class PostFormModel
    {

        //[Required]
        //[MinLength(MinTitleLenght)]
        //[MaxLength(MaxTitleLenght)]
        [Required(ErrorMessage = "Name is requared")]
        [StringLength(50, MinimumLength = 2 , ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(MinContentLength)]
        [MaxLength(MaxContentLength)]
        public string Content { get; set; } = null!;
    }
}
