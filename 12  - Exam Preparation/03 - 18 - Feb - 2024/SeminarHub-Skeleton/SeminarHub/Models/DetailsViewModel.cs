﻿namespace SeminarHub.Models
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string Topic { get; set; } = string.Empty;
        public string DateAndTime { get; set; } = string.Empty;

        public string Duration { get; set; }= string.Empty;

        public string Lecturer { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public string Organizer { get; set; } = string.Empty;
    }
}
