using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Homies.Common.ValidationConstant;

namespace Homies.Data.Models
{
    public class Event
    {
        [Key]
        [Comment("Id")]
        public int Id { get; set; }

        [Required]
        [Comment("Name")]
        [MaxLength(EventNameMaxLenght)]

        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("Description")]
        [MaxLength(EventDescriptionMaxLenght)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("OrganiserId")]
        public string OrganiserId { get; set; } = string.Empty;

        [Required]
        [Comment("Organiser")]
        [ForeignKey(nameof(OrganiserId))]
        public IdentityUser Organiser { get; set; } = null!;

        [Required]
        [Comment("CreateOn")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("Start")]
        public DateTime Start { get; set; }

        [Required]
        [Comment("End")]
        public DateTime End { get; set; }

        [Required]
        [Comment("TypeId")]
        public int TypeId { get; set; }

        [Required]
        [Comment("Type")]
        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; } = null!;

        [Comment("EventParticipants")]
        public IList<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}

