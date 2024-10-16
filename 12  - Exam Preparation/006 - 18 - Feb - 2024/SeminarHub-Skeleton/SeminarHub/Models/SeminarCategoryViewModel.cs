using Microsoft.EntityFrameworkCore;
using SeminarHub.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
    public class SeminarCategoryViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("Name")]
        public string Name { get; set; } = string.Empty;

    }
}
