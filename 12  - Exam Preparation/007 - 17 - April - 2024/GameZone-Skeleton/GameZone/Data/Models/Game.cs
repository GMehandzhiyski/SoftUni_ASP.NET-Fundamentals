using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameZone.Common.ValidationConstants;

namespace GameZone.Data.Models
{
    public class Game
    {
        [Key]
        [Comment("Id")]
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
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("PublisherId")]
        public string PublisherId { get; set; } = string.Empty;

        [Required]
        [Comment("PublisherId")]
        [ForeignKey(nameof(PublisherId))]
        public IdentityUser Publisher { get; set; } = null!;

        [Required]
        [Comment("ReleasedOn")]
        public DateTime ReleasedOn { get; set; }

        [Required]
        [Comment("GenreId")]
        public int GenreId { get; set; }

        [Required]
        [ForeignKey(nameof(GenreId))]
        [Comment("Genre")]
        public Genre Genre { get; set; } = null!;

        [Comment("Collection of GamerGame")]
        public ICollection<GamerGame> GamersGames { get; set; } = new List<GamerGame>();
    }
}
