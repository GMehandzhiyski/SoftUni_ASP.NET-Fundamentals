using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Models
{
    public class DeskDetailsViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("ProductName")]
        public string ProductName { get; set; } = string.Empty;

        [Comment("Description")]
        public string Description { get; set; } = string.Empty;

        [Comment("Price")]
        public decimal Price { get; set; }

        [Comment("ImageUrl")]
        public string? ImageUrl { get; set; } = string.Empty;

        [Comment("AddedOn")]
        public string AddedOn { get; set; } = string.Empty;

        [Comment("CategoryName")]
        public string CategoryName { get; set; } = string.Empty;

        [Comment("Seller")]
        public string Seller { get; set; } = string.Empty;

        [Comment("HasBought")]
        public bool HasBought { get; set; }

    }
}
