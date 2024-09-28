using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Common.ValidationConstants;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Primary Key")]
        public int Id { get; set; }

        [Required]
        [Comment("Name")]
        [MaxLength(CategoryNameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        [Comment("Name")]
        public ICollection<Ad> Ads { get; set; } =  new List<Ad>();
    }
}
