using System.ComponentModel.DataAnnotations;
using static Homies.Common.ValidationConstant;

namespace Homies.Data.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TypeNameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Event> Events { get; set; } = new List<Event>();
    }
}

