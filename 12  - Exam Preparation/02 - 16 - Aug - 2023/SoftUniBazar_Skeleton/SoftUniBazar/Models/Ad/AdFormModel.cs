using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Models.Category;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Common.ValidationConstants;
using static SoftUniBazar.Common.ValidationErrorMessages;

namespace SoftUniBazar.Models.Ad
{

    public class AdFormModel
    {
        [Required (ErrorMessage = ErrorMessegeName)]
        [Comment("Name")]
        [StringLength(AdNameMaxLenght,
                      MinimumLength = AdNameMinLenght,
                      ErrorMessage = ErrorMessegeNameLenght)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessegeDescription)]
        [Comment("Description")]
        [StringLength(AdDescriptionMaxLenght, 
                      MinimumLength =AdDescriptionMinLenght,
                      ErrorMessage = ErrorMessegeDescriptionLenght)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessegeImageUrl)]
        [Comment("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessegePrice)]
        [Comment("Price")]
        public decimal Price { get; set; }

        [Required]
        [Comment("CategotyId")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();


    }
}
