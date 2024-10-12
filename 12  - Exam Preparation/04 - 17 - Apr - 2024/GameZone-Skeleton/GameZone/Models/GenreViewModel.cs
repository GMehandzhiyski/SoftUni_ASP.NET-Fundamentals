using Microsoft.EntityFrameworkCore;

namespace GameZone.Models
{
    public class GenreViewModel
    {
        [Comment("Id")]
        public int Id { get; set; }

        [Comment("Name")]
        public string Name { get; set; }= string.Empty;
    }
}
