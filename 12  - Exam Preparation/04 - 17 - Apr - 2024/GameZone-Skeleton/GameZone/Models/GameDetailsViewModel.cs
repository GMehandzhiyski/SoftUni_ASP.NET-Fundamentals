using Microsoft.EntityFrameworkCore;

namespace GameZone.Models
{
    public class GameDetailsViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("Title")]
        public string Title { get; set; } = string.Empty;

        [Comment("Description")]
        public string Description { get; set; } = string.Empty;

        [Comment("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Comment("Publisher")]
        public string Publisher { get; set; } = null!;

        [Comment("ReleasedOn")]
        public string ReleasedOn { get; set; } = string.Empty;


        [Comment("Genre")]
        public string Genre { get; set; } = string.Empty;
    }
}
