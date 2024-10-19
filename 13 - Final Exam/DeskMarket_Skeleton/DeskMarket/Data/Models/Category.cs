using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ValidationConstants;

namespace DeskMarket.Data.Models
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

        [Comment("Collection of Products")]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

//•	Has Products – a collection of type Product
