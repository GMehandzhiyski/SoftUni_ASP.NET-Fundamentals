using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Data.Models
{
    public class AdBuyer
    {
        [Required]
        [Comment("BuyerId")]
        public string BuyerId { get; set; } = string.Empty;

        [Required]
        [Comment("BuyerId Foreing Key")]
        [ForeignKey(nameof(BuyerId))]
        public IdentityUser Buyer { get; set;} = null!;

        [Required]
        [Comment("AsId")]
        public int AdId { get; set; }

        [Required]
        [Comment("AdId Foreing Key")]
        [ForeignKey(nameof(AdId))]
        public Ad Ad { get; set; } = null!;
    }
}
