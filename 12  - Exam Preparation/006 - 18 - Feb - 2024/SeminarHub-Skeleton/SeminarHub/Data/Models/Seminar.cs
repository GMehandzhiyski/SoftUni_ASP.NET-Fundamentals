using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
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
        [MaxLength(SeminarTopicMaxLenght)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [Comment("Lecturer")]
        [MaxLength(SeminarLecturerMaxLenght)]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [Comment("Details")]
        [MaxLength(SeminarDetailsMaxLenght)]
        public string Details { get; set; } = string.Empty;

        [Required]
        [Comment("OrganiserId")]
        public string OrganizerId { get; set; } = string.Empty;

        [Required]
        [Comment("OrganiserId")]
        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        [Comment("Data and Time")]
        public DateTime DateAndTime { get; set; }

        [Required]
        [Comment("Duration")]
        [MaxLength(SeminarDurationMaxLenght)]
        public int Duration { get; set; }

        [Required]
        [Comment("CategoryId")]
        public int CategoryId { get; set; }

        [Required]
        [Comment("CategoryId")]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;


        [Comment("SeminarParticipants Collection")]
        public ICollection<SeminarParticipant> SeminarsParticipants = new List<SeminarParticipant>();

        [Required]
        [Comment("Soft delete")]
        public bool isDelete { get; set; }
    }
}
