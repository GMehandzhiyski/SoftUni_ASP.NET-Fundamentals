namespace Homies.Data.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EventParticipant
{
    [Required]
    [Comment("Helper Id")]
    [ForeignKey(nameof(Helper))]
    public string HelperId { get; set; } = null!;

    [Comment("Helper")]
    public IdentityUser Helper = null!;

    [Required]
    [Comment("Event Id")]
    [ForeignKey(nameof(Event))]
    public int EventId { get; set; }

    [Comment("Event")]
    public Event Event { get; set; } = null!;
}
