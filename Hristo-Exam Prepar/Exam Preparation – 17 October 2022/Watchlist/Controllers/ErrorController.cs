namespace Watchlist.Controllers;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Models.Error;

public class ErrorController : Controller
{
    public IActionResult Index(StatusCodeResult statusCode)
    {

        ErrorStatusViewModel model = new ErrorStatusViewModel()
        {
            StatusCode = statusCode.StatusCode,
            Message = statusCode.StatusCode.ToString(),
        };

        return View(model);
    }
}
