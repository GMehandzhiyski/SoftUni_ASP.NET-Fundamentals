using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Models
{
    public class DeskCartViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("ProductName")]
        public string ProductName { get; set; } = string.Empty;

        [Comment("Price")]
        public decimal Price { get; set; }

        [Comment("ImageUrl")]
        public string? ImageUrl { get; set; } = string.Empty;

    }
}
