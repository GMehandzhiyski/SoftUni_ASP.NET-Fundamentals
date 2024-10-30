using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Models
{
    public class DeskCategoryViewModel
    {

        [Comment("Id")]
        public int Id { get; set; }

        [Comment("Name")]
        public string Name { get; set; } = string.Empty;
    }
}
