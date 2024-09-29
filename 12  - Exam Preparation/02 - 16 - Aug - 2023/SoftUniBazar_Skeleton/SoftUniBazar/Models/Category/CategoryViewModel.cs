using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Models.Category
{
    public class CategoryViewModel
    {
        [Comment("Primary Key")]
        public int Id { get; set; }

        [Required]
        [Comment("Name")]
        public string Name { get; set; } = string.Empty;

    }
}
