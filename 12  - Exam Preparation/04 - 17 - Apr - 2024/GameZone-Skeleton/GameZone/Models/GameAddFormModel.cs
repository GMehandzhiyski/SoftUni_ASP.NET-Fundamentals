using GameZone.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static GameZone.Common.ValidationConstants;
using static GameZone.Common.ValidationErrors;

namespace GameZone.Models
{
    public class GameAddFormModel
    {

        [Required(ErrorMessage = ErrorMessageTitle)]
        [Comment("Title")]
        [Display(Name = "Game Title")]
        [StringLength(GameTitleMaxLenght,
                       MinimumLength = GameTitleMinLenght,
                        ErrorMessage = ErrorMessageTitleLenght)]
        public string Title { get; set; } = string.Empty;


        [Comment("ImageUrl")]
        [Display(Name = "ImageURL")]
        public string ImageUrl { get; set; } = string.Empty;
        
        [Required(ErrorMessage = ErrorMessageDescription)]
        [Comment("Description")]
        [StringLength(GameDescriptionMaxLenght,
                       MinimumLength = GameDescriptionMinLenght,
                        ErrorMessage = ErrorMessageDescriptionLenght)]
        public string Description { get; set; } = string.Empty;

       
        [Comment("PublisherId")]
        public string PublisherId { get; set; } = string.Empty;


        [Required(ErrorMessage = ErrorMessageReleasedOn)]
        [Comment("ReleasedOn")]
        [Display(Name = "Released On")]
        public string ReleasedOn { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageGenreId)]
        [Comment("GenreId")]
        [Display(Name = "Select Genre")]
        public int GenreId { get; set; }

        [Comment("List of GamersGames")]
        public IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
    }
}
