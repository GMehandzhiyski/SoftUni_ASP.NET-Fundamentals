using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Common.ValidationConstants;

namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        [Key]
        [Comment("Id")]
        public int Id { get; set; }

        [Required]
        [Comment("Topic")]
        [MaxLength(SeminarTopicMaxLength)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [Comment("Lecturer")]
        [MaxLength(SeminarLecturerMaxLength)]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [Comment("Details")]
        [MaxLength(SeminarDetailsMaxLength)]
        public string Details { get; set; } = string.Empty;

        [Required]
        [Comment("OrganizerId")]
        public string OrganizerId { get; set; } = string.Empty;


        [Comment("Organizer")]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        [Comment("DateAndTime")]
        public DateTime DateAndTime { get; set; }

        [Comment("Duration")]
        [MaxLength(SeminarDurationMaxLength)]
        public int Duration { get; set; }

        [Required]
        [Comment("CategoryId")]
        public int CategoryId { get; set; }

        [Required]
        [Comment("ForeingKey CategoryId")]
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        [Comment("SeminarsParticipants")]
        public virtual IEnumerable<SeminarParticipant> SeminarsParticipants { get; set; }
    }
}

