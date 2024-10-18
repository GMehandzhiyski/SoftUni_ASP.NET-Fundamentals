using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static GameZone.Common.ValidationConstants;
using static GameZone.Common.ValidationErrors;

namespace GameZone.Models
{
    public class GameAddFormModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessageTitle)]
        [Comment("Title")]
        [StringLength(GameTitleMaxLenght,
                      MinimumLength = GameTitleMinLenght,
                      ErrorMessage = ErrorMessageTitleLength)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageDescription)]
        [Comment("Description")]
        [StringLength(GameDescriptionMaxLenght,
                      MinimumLength = GameTitleMinLenght,
                      ErrorMessage = ErrorMessageDescriptionLength)]
        public string Description { get; set; } = string.Empty;

        [Comment("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Comment("PublisherId")]
        public string PublisherId { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageReleasedOn)]
        [Comment("ReleasedOn")]
        public string ReleasedOn { get; set; } = string.Empty ;

        [Required(ErrorMessage = ErrorMessageGenreId)]
        [Comment("GenreId")]
        public int GenreId { get; set; }

        public ICollection<GameGenreViewModel> Genres { get; set; }  = new List<GameGenreViewModel>();
    }
}
