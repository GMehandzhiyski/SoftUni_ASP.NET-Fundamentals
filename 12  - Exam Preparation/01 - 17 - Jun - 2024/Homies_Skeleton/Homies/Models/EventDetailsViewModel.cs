using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class EventDetailsViewModel
    {
      
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Start { get; set; } = null!;
        public string Organiser { get; set; } = string.Empty;

        public string End { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public string Type { get; set; } = string.Empty;

    }
}
