using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SoftUniBazar.Models.Ad
{
    public class AdViewModel
    {
        [Comment("Ad class Primary Key")]
        public int Id { get; set; }

  
        [Comment("Name")]
        public string Name { get; set; } = string.Empty;

    
        [Comment("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

  
        [Comment("CreatedOn")]
        public string CreatedOn { get; set; } = string.Empty;

   
        [Comment("CategotyId")]
        public string Category { get; set; }    =string.Empty;



        [Comment("Description")]
        public string Description { get; set; } = string.Empty;

   
        [Comment("Price")]
        public decimal Price { get; set; }

        [Comment("Owner")]
        public string Owner { get; set; } = null!;

    }
}
