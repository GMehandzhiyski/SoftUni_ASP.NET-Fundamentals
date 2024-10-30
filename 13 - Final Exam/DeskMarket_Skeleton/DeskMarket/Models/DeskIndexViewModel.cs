using DeskMarket.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Models
{
    public class DeskIndexViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("ProductName")]
        public string ProductName { get; set; } = string.Empty;

        [Comment("Price")]
        public decimal Price { get; set; }

        [Comment("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Comment("IsSeller")]
        public bool IsSeller { get; set; }

        [Comment("HasBought")]
        public bool HasBought { get; set; }

    }
}
