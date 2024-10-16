using Microsoft.AspNetCore.Mvc;
using SeminarHub.Contract;

namespace SeminarHub.Controllers
{
    public class SeminarController : Controller
    {
        private readonly ISeminarService data;

        public SeminarController(ISeminarService _data)
        {
            data = _data;
        }


    }
}
