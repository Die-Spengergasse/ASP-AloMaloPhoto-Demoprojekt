using Microsoft.AspNetCore.Mvc;
using Spg.AloMalo.Application.MockController;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces;

namespace Spg.AloMalo.Api.Controllers
{
    [Route("api/album")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet("ok")]
        public IActionResult GetAlbums()
        {
            return _albumService
                .GetAllOk()
                .ResultOrExceptions<IQueryable<AlbumDto>, AlbumSerivceException, ArgumentException>(
                    r => new OkObjectResult(r),
                    e => new BadRequestObjectResult($"{e.GetType().Name} - {e.Message}"),
                    e => new NotFoundObjectResult($"{e.GetType().Name} - {e.Message}"));
        }

        [HttpGet("400")]
        public IActionResult GetAlbums400()
        {
            return _albumService
                .GetAll400()
                .ResultOrExceptions<IQueryable<AlbumDto>, AlbumSerivceException, ArgumentException>(
                    r => new OkObjectResult(r),
                    e => new BadRequestObjectResult($"{e.GetType().Name} - {e.Message}"),
                    e => new NotFoundObjectResult($"{e.GetType().Name} - {e.Message}"));
        }

        [HttpGet("404")]
        public IActionResult GetAlbums404()
        {
            return _albumService
                .GetAll404()
                .ResultOrExceptions<IQueryable<AlbumDto>, AlbumSerivceException, ArgumentException>(
                    r => new OkObjectResult(r),
                    e => new BadRequestObjectResult($"{e.GetType().Name} - {e.Message}"),
                    e => new NotFoundObjectResult($"{e.GetType().Name} - {e.Message}"));
        }

        [HttpGet("mock")]
        public IActionResult GetAlbumsMock()
        {
            AlbumControllerMock mock = new AlbumControllerMock(_albumService);
            var result = mock.GetAlbums();

            return Ok(result);
        }
    }
}
