using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
    public class CategoryViewModel
    {
       
        [Comment("Id")]
        public int Id { get; set; }

     
        [Comment("Name")]
        public string Name { get; set; } = string.Empty;
    }
}
