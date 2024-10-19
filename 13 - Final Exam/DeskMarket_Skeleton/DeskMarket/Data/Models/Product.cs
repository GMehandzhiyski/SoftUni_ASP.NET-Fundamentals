using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DeskMarket.Common.ValidationConstants;

namespace DeskMarket.Data.Models
{
    public class Product
    {
        [Key]
        [Comment("Id")]
        public int Id { get; set; }

        [Required]
        [Comment("ProductName")]
        [MaxLength(ProductProductNameMaxLenght)]
        private string ProductName { get; set; } = string.Empty;

        [Required]
        [Comment("Description")]
        [MaxLength(ProductDescriptionMaxLenght)]
        private string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Comment("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("SellerId")]
        public string SellerId { get; set; } = string.Empty;

        [Required]
        [Comment("Seller")]
        [ForeignKey(nameof(SellerId))]
        public IdentityUser Seller { get; set; } = null!;
        
        [Required]
        [Comment("AddedOn")]
        public DateTime AddedOn { get; set; }

        [Required]
        [Comment("CategoryId")]
        public int CategoryId { get; set; }

        [Required]
        [Comment("Category")]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Comment("IsDeleted")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Collection of ProductsClients")]
        public ICollection<ProductsClients> ProductsClients { get; set; } = new List<ProductsClients>();
    }

}




