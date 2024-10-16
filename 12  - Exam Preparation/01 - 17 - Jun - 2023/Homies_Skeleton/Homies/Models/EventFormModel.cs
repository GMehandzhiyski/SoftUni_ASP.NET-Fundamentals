using System.ComponentModel.DataAnnotations;
using static Homies.Common.ValidationConstant;
using static Homies.Common.ValidationError;

namespace Homies.Models
{
    public class EventAddViewModel
    {
        
        [Required(ErrorMessage = ErrorMessageName)]
        [StringLength(
            EventNameMaxLenght,
            MinimumLength = EventNameMinLenght,
            ErrorMessage = ErrorMessageNameLength)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageDescription)]
        [StringLength(
            EventDescriptionMaxLenght, 
            MinimumLength = EventDescriptionMinLenght,
            ErrorMessage = ErrorMessageDescriptionLength)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageStart)]
        public string Start { get; set; } = null!;

        [Required(ErrorMessage = ErrorMessageEnd)]
        public string End { get; set; } = null!;

        public string? OrganiserId { get; set; }

        public IEnumerable<EventTypeViewModel> Types { get; set; } = new List<EventTypeViewModel>();

        [Required(ErrorMessage = ErrorMessageTypeId)]
        public int TypeId { get; set; }
    }
}
