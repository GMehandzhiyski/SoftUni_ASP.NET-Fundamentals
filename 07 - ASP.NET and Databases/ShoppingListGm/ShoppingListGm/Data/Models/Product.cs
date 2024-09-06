using System.ComponentModel.DataAnnotations;
using ShoppingListGm.Validation;


namespace ShoppingListGm.Data.Models
{
	public class Product
	{
        [Key]
        public int Id { get; set; }

        [Required]
		[MaxLength(ValidationConstants.MaxLengthProductName)]
        public string ProductName { get; set; } = string.Empty;

        public List<ProductNote> ProductNotes { get; set; } = new List<ProductNote>();

    }
}
