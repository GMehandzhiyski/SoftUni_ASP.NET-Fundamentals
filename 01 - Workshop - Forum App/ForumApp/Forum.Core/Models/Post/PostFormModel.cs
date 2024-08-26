using System.ComponentModel.DataAnnotations;

namespace Forum.Core.Models.Post
{
    using static Forum.Common.Validation.ValidationConstants.Post;
    public class PostFormModel
    {

        [Required]
        [MinLength(MinTitleLenght)]
        [MaxLength(MaxTitleLenght)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(MinContentLength)]
        [MaxLength(MaxContentLength)]
        public string Content { get; set; } = null!;
    }
}
