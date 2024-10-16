using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Common.ValidationConstants;

namespace SeminarHub.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Id")]
        public int Id { get; set; }

        [Required]
        [Comment("Name")]
        [MaxLength(CategoryNameMaxLenght)]
        public string Name { get; set; } = string.Empty;  
        
        public IList<Seminar> Seminars { get; set; } = new List<Seminar>();
    }
}

