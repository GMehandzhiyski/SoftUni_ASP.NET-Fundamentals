using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
    public class EventParticipant
    {
        [Required]
        [Comment("HelperId")]
        public string HelperId { get; set; } = string.Empty;

        [ForeignKey(nameof(HelperId))]
        [Comment("Helper")]
        public IdentityUser Helper { get; set; } = null!;

        [Required]
        [Comment("EventId")]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        [Comment("Event")]
        public Event Event { get; set; } = null!;


    }
}



