using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameZone.Common.ValidationConstants;

namespace GameZone.Data.Models
{
    public class Game
    {
        [Key]
        [Comment("id")]
        public int Id { get; set; }

        [Required]
        [Comment("Title")]
        [MaxLength(GameTitleMaxLenght)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Comment("Description")]
        [MaxLength(GameDescriptionMaxLenght)]
        public string Description { get; set; } = string.Empty;

        [Comment("ImageUrl")]
        public string ImageUrl { get; set; }

        [Required]
        [Comment("PublisherId")]
        public string PublisherId { get; set; } = string.Empty;

        [ForeignKey(nameof(PublisherId))]
        public IdentityUser Publisher { get; set; } = null!;

        [Required]
        [Comment("ReleasedOn")]
        public DateTime ReleasedOn { get; set; }

        [Required]
        [Comment("GenreId")]
        public int GenreId { get; set; }

        [Required]
        [Comment("Genre")]
        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; } = null!;

        [Comment("List of GamersGames")]
        public IEnumerable<GamersGame> GamersGames { get; set; } = new List<GamersGame>();
    }
}
