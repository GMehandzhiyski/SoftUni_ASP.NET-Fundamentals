using Microsoft.EntityFrameworkCore;

namespace SeminarHub.Models
{
    public class SeminarDeleteVIewModel
    {

        [Comment("Id")]
        public int Id { get; set; }

        [Comment("Topic")]
        public string Topic { get; set; } = string.Empty;

        [Comment("Data and Time")]
        public DateTime DateAndTime { get; set; }

        [Comment("OrganizerId")]
        public string OrganizerId { get; set; } = string.Empty;
    }
}
