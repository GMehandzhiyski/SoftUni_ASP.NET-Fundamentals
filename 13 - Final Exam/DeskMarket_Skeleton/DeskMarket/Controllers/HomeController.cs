using DeskMarket.Extensions;
using DeskMarket.Models;
using DeskMarket.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static DeskMarket.Common.DateConstants;

namespace DeskMarket.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
           
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
