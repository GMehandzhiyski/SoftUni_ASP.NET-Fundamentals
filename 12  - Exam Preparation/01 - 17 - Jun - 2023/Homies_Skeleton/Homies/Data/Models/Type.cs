using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Homies.Common.ValidationConstant;

namespace Homies.Data.Models
{
    public class Type
    {
        [Key]
        [Comment("Id")]
        public int Id { get; set; }

        [Required]
        [Comment("Name")]
        [MaxLength(TypeNameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        [Comment("Events")]
        public IEnumerable<Event> Events { get; set; } = new List<Event>();
    }
}

