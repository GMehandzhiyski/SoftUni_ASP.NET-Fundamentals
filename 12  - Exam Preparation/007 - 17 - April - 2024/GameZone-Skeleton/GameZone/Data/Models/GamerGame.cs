using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace GameZone.Data.Models
{
    public class GamerGame
    {
        [Required]
        [Comment("GameId")]
        public int GameId { get; set; }

        [Required]
        [Comment("Game")]
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; } = null!;

        [Required]
        [Comment("GamerId")]
        public string GamerId { get; set; } = null!;

        [Required]
        [Comment("Gamer")]
        [ForeignKey(nameof(GamerId))]
        public IdentityUser Gamer { get; set; } = null!;
    }
}
