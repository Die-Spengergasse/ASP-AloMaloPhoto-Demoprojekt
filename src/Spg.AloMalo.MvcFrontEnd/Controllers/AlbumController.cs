using Microsoft.AspNetCore.Mvc;
using Spg.AloMalo.Application.Services;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Error;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.MvcFrontEnd.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            // TODO: Liste laden
            ErrorCheck<IQueryable<Album>> data = _albumService.GetAllOk();
            return View(data.Value);
        }

        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost()]
        public IActionResult Create(CreateAlbumCommand model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
