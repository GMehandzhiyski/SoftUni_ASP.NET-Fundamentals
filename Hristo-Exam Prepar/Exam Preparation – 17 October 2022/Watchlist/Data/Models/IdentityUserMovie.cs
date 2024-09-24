namespace Watchlist.Data.Models;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;



public class IdentityUserMovie
{
    [ForeignKey(nameof(Movie))]
    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;

    [ForeignKey(nameof(IdentityUser))]
    public string IdentityUserId { get; set; } = null!;

    public IdentityUser IdentityUser { get; set; } = null!;  

}
