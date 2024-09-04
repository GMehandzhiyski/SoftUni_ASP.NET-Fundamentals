using System.ComponentModel.DataAnnotations;
using ShoppingListGm.Validation;

namespace ShoppingListGm.Models
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Field {0} is required")]
		[StringLength(ValidationConstants.MaxLengthProductName,MinimumLength = ValidationConstants.MinLenghtProductName, ErrorMessage = "Field {0} must be between {2} and {1} symbols")]
		public string ProductName { get; set; } = string.Empty;

	}
}
