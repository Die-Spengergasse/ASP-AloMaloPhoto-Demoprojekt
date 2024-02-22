using Microsoft.AspNetCore.Mvc;
using Spg.AloMalo.DomainModel.Commands;

namespace Spg.AloMalo.MvcFrontEnd.Controllers
{
    public class AlbumController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            // TODO: Liste laden
            return View();
        }

        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Create(CreateAlbumCommand model)
        {
            return View();
        }
    }
}
