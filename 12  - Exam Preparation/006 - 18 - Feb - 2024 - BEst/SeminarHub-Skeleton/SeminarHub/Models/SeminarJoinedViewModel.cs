﻿using Microsoft.EntityFrameworkCore;

namespace SeminarHub.Models
{
    public class SeminarJoinedViewModel
    {

        [Comment("Id")]
        public int Id { get; set; }

        [Comment("Topic")]
        public string Topic { get; set; } = string.Empty;

        [Comment("Lecturer")]
        public string Lecturer { get; set; } = string.Empty;

        [Comment("Organizer")]
        public string Organizer { get; set; } = string.Empty;

        [Comment("Data and Time")]
        public string DateAndTime { get; set; } = string.Empty;
    }
}
