using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class GameGenreViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("Name")]
        public string Name { get; set; } = string.Empty;
    }
}
