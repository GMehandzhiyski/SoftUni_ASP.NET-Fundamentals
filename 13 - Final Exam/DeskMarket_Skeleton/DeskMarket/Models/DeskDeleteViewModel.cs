using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Models
{
    public class DeskDeleteViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("ProductName")]
        public string ProductName { get; set; } = string.Empty;

        [Comment("Seller")]
        public string Seller { get; set; } = string.Empty ;

        [Comment("SellerId")]
        public string SellerId { get; set; } = string.Empty;
    }
}
