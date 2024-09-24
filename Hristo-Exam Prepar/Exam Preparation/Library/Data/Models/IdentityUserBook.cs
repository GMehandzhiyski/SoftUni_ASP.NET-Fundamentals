using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Models
{
    public class IdentityUserBook
    {
        
        public Book Book { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
 
        public IdentityUser Collector { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Collector))]
        public string CollectorId { get; set; } = null!;
    }
}
