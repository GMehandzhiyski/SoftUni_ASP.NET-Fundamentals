using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models
{
    public class SeminarParticipant
    {
        [Required]
        [Comment("SeminarId")]
        public string SeminarId { get; set; }   = string.Empty;

        [Required]
        [Comment("Seminar")]
        [ForeignKey(nameof(SeminarId))]

        public Seminar Seminar { get; set; } = null!;

        [Required]
        [Comment("ParticipantId")]
        public string ParticipantId { get; set; } = null!;

        [Required]
        [Comment("Participant")]
        [ForeignKey(nameof(ParticipantId))]

        public IdentityUser Participant { get; set; } = null!;


    }
}
