using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SoftUniBazar.Common.ValidationConstants;

namespace SoftUniBazar.Data.Models
{
    public class Ad
    {
        [Key]
        [Comment("Ad class Primary Key")]
        public int Id { get; set; }

        [Required]
        [Comment("Name")]
        [MaxLength(AdNameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("Discrioption")]
        [MaxLength(AdDescriptionMaxLenght)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Price")]
        public decimal Price { get; set; }

        [Required]
        [Comment("OwnerId")]
        public string OwnerId { get; set; } = string.Empty ;


        [Required]
        [Comment("Foreing Key OwnerId")]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        [Comment("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("CategoryId")]
        public int CategoryId { get; set; }


        [Comment("Foreing Key CategoryId")]
        [ForeignKey(nameof(CategoryId))]

        [Required]
        public Category Category { get; set; } = null!;


    }
}
