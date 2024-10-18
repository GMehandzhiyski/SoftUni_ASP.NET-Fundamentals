using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class GameDeleteViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("Title")]
        public string Title { get; set; } = string.Empty;

        [Comment("Publisher")]
        public string Publisher { get; set; } = string.Empty;
    }
}
