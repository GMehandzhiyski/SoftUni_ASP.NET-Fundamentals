using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeminarHub.Contract;
using SeminarHub.Models;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
       private readonly ISeminarService data;

        public SeminarController(ISeminarService _data)
        {
            data = _data;
        }

        [HttpGet]
        public async Task<IActionResult> Add() 
        {

            try
            {
                AddFormModel model = new AddFormModel();

                return View(model);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
            
        }
    }
}
