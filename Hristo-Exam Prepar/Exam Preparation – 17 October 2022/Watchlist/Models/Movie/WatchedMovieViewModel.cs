using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

namespace Watchlist.Models.Movie
{
    public class WatchedMovieViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string Rating { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;


    }
}

//< div class= "card col-4" style = "width: 20rem; " >
//            < img class= "card-img-top" style = "width: 18rem;"
//             src = "@movie.ImageUrl" alt = "Movie Image" >
//            < div class= "card-body" >

//                < h5 class= "card-title mt-1" > @movie.Title </ h5 >
//                < p class= "mb-0" > Director: @movie.Director </ p >
//                < p class= "mb-0" > Rating: @movie.Rating </ p >
//                < p > Genre: @movie.Genre </ p >
//            </ div >

//            < form class= "input-group-sm" asp - route - movieId = "@movie.Id" asp - controller = "Movies" asp - action = "RemoveFromCollection" method = "post" >
//                < input class= "fs-6 btn btn-success mb-3 p-2" type = "submit" value = "Mark as Unwatched" />
//            </ form >
//        </ div >