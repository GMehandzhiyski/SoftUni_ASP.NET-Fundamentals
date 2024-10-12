using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static GameZone.Common.ValidationConstants;

namespace GameZone.Data.Models
{
    public class Genre
    {
        [Key]
        [Comment("Id")]
        public int Id { get; set; }

        [Required]
        [Comment("Name")]
        [MaxLength(GenreNameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("Games")]
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();

    }
}

