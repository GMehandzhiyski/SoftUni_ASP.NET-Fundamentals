using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskMarket.Data.Models
{
    public class ProductsClients
    {
        [Required]
        [Comment("ProductId")]
        public int ProductId { get; set; }

        [Required]
        [Comment("Product")]
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Required]
        [Comment("ClientId")]
        public string ClientId { get; set; } = string.Empty;

        [Required]
        [Comment("Client")]
        [ForeignKey(nameof(ClientId))]
        public IdentityUser ClientName { get; set; } = null!;
    }
}

