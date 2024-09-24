namespace Watchlist.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Contracts;
using Watchlist.Extensions;
using Watchlist.Models.Error;
using Watchlist.Models.Genre;
using Watchlist.Models.Movie;
using static Common.ValidationConstants.Movie;

[Authorize]
public class MoviesController : Controller
{
    private readonly IMovieService movieService;
    private readonly IGenreService genreService;

    public MoviesController(IMovieService movieService, IGenreService genreService)
    {
        this.movieService = movieService;   
        this.genreService = genreService;
    }

    public async Task<IActionResult> All()
    {
        try
        {
            ICollection<AllMoviesViewModel> movies = await movieService.GetAllAsync();
            throw new Exception();
            return View(movies);
        }
        catch (Exception)
        {

            return RedirectToAction("Index", "Error", StatusCodes.Status400BadRequest);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {

        try
        {
            FormMovieViewModel model = new FormMovieViewModel();

            ICollection<GenreViewModel> genres = await genreService.GetAllAsync();

            model.Genres = genres;

            return View(model);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
       
    }

    [HttpPost]
    public async Task<IActionResult> Add(FormMovieViewModel model)
    {

        try
        {
           if(double.Parse(model.Rating) < MinRating || double.Parse(model.Rating) > MaxRating)
            {
                ModelState.AddModelError("Rating", $"Rating must be between ${MinRating} and ${MaxRating}");
                ICollection<GenreViewModel> genres = await genreService.GetAllAsync();

                model.Genres = genres;
                return View(model);
            }

           if(!ModelState.IsValid)
            {
                ICollection<GenreViewModel> genres = await genreService.GetAllAsync();
                model.Genres = genres;
                return View(model);
            }

            await movieService.AddAsync(model);

            return RedirectToAction("All", "Movies");   

        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }

    }

    [HttpGet]
    public async Task<IActionResult> Watched()
    {
        try
        {
            ICollection<WatchedMovieViewModel> movies = await movieService.GetUserWatchedMoviesAsync(User.GetId());

            return View(movies);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToCollection(int movieId)
    {
        try
        {
            await movieService.AddMovieToUserCollectionAsync(User.GetId(), movieId);

            return RedirectToAction("All", "Movies");
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCollection(int movieId)
    {
        try
        {
            await movieService.RemoveMovieFromUserCollectionAync(User.GetId(), movieId);

            return RedirectToAction("All", "Movies");
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
        }
    }
}
