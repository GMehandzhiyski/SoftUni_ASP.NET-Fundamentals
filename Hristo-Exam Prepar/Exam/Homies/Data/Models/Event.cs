namespace Homies.Data.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.ValidationConstants.Event;
using static Common.DateTimeParseFormats;

[Comment("The event entity")]
public class Event
{
    public Event()
    {
        this.EventsParticipants = new List<EventParticipant>();
        this.CreatedOn = DateTime.Now;
    }

    [Key]
    [Comment("Event primary key")]
    public int Id { get; set; }

    [Required]
    [Comment("Event name")]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    [Comment("Event description")]
    [MinLength(MinDescriptionLength)]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; } = null!;

    [Required]
    [Comment("Organiser Id")]
    [ForeignKey(nameof(Organiser))]
    public string OrganiserId { get; set; } = null!;

    [Required]
    [Comment("Event Organiser")]
    public IdentityUser Organiser { get; set; } = null!;

    [Required]
    [Comment("Date of event creation")]
    [DisplayFormat(DataFormatString = DefaultTimeFormat, ApplyFormatInEditMode = true)]
    public DateTime CreatedOn { get; set; }

    [Required]
    [Comment("Date of event start")]
    [DisplayFormat(DataFormatString = DefaultTimeFormat, ApplyFormatInEditMode = true)]
    public DateTime Start { get; set; }

    [Required]
    [Comment("Date of event end")]
    [DisplayFormat(DataFormatString = DefaultTimeFormat, ApplyFormatInEditMode = true)]
    public DateTime End { get; set; }

    [Required]
    [Comment("Type Id")]
    [ForeignKey(nameof(Type))]
    public int TypeId { get; set; }

    [Required]
    [Comment("Event type")]
    public Type Type { get; set; } = null!;

    [Comment("Participants of the event")]
    public ICollection<EventParticipant> EventsParticipants { get; set; }

}
