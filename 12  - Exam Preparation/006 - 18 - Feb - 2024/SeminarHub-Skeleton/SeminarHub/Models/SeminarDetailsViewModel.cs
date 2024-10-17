using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
    public class SeminarDetailsViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("Topic")]
        public string Topic { get; set; } = string.Empty;

        [Comment("Lecturer")]
        public string Lecturer { get; set; } = string.Empty;

        [Comment("Duration")]
        public int Duration { get; set; }

        [Comment("Details")]
        public string Details { get; set; } = string.Empty;

        [Comment("Organizer")]
        public string Organizer { get; set; } = string.Empty;

        [Comment("Data and Time")]
        public string DateAndTime { get; set; } = string.Empty;

        public string Category { get; set; } = null!;
    }
}
