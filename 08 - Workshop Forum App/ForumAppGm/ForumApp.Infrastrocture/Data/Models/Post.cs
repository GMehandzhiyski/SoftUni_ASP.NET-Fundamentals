using System.ComponentModel.DataAnnotations;
using static ForumApp.Common.Constants.ValidationConstant;

namespace ForumApp.Infrastructure.Data.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxLengthTitle)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(MaxLengthContent)]
        public string Content { get; set; } = string.Empty;
    }
}
