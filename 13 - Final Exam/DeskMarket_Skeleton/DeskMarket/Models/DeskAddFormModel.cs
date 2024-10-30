using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ValidationConstants;
using static DeskMarket.Common.ValidationErrors;

namespace DeskMarket.Models
{
    public class DeskAddFormModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessageProductName)]
        [Comment("ProductName")]
        [StringLength(ProductProductNameMaxLenght,
                      MinimumLength = ProductProductNameMinLenght,
                      ErrorMessage = ErrorMessageProductNameLength)]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessageDescription)]
        [Comment("Description")]
        [StringLength(ProductDescriptionMaxLenght,
                      MinimumLength = ProductDescriptionMinLenght,
                      ErrorMessage = ErrorMessageDescriptionLength)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessagePrice)]
        [Comment("Price")]
        [Range(ProductPriceMinLenght, ProductPriceMaxLenght, ErrorMessage = ErrorMessagePriceLength)]
        public string Price { get; set; } = string.Empty;

        [Comment("ImageUrl")]
        public string? ImageUrl { get; set; } = string.Empty;


        [Required(ErrorMessage = ErrorMessageAddedOn)]
        [Comment("AddedOn")]
        public string AddedOn { get; set; } = string.Empty;

        [Comment("CategoryId")]
        public int CategoryId { get; set; }

        [Required]
        [Comment("SellerId")]
        public string SellerId { get; set; } = string.Empty;


        [Comment("Collection of ProductsClients")]
        public ICollection<DeskCategoryViewModel> Categories { get; set; } = new List<DeskCategoryViewModel>();
    }
}
