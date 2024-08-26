namespace Forum.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Forum.Common.Validation.ValidationConstants.Post;
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

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
