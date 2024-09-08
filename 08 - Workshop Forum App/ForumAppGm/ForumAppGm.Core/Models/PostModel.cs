using System.ComponentModel.DataAnnotations;
using static ForumApp.Common.Constants.ValidationConstant;
namespace ForumAppGm.Core.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessageTitle)]
        [StringLength(MaxLengthTitle, MinimumLength = MinLengthTitle, ErrorMessage = ErrorMessageTitleLength)]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = ErrorMessageContext)]
        [StringLength(MaxLengthContent, MinimumLength = MinLengthContent, ErrorMessage = ErrorMessageContextLength)]
        public string Content { get; set; } = string.Empty;
    }
}
