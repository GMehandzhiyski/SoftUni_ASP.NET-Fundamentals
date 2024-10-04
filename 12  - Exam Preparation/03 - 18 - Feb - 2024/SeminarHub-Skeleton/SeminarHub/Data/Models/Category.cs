using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Common.ValidationConstant;

namespace SeminarHub.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Id")]
        public int Id { get; set; }

        [Required]
        [Comment("Name")]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("Seminars")]
        public IEnumerable<Seminar> Seminars = new List<Seminar>(); 
    }
}
