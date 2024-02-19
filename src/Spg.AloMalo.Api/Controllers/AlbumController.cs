using Microsoft.AspNetCore.Mvc;
using Spg.AloMalo.Application.MockController;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Api.Controllers
{
    [Route("api/[controller]")]
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
                .Error<IQueryable<Album>, AlbumSerivceException>(e => new BadRequestObjectResult($"{e.GetType().Name} - {e.Message}"))
                .Or.Error<IQueryable<Album>, ArgumentException>(e => new NotFoundObjectResult($"{e.GetType().Name} - {e.Message}"))
                .Or.Ok(r => new OkObjectResult(r))
                .Result;

            //return _albumService
            //    .GetAllOk()
            //    .ResultOrExceptions<IQueryable<Album>, AlbumSerivceException, ArgumentException>(
            //        r => new OkObjectResult(r),
            //        e => new BadRequestObjectResult($"{e.GetType().Name} - {e.Message}"),
            //        e => new NotFoundObjectResult($"{e.GetType().Name} - {e.Message}"));
        }

        [HttpGet("400")]
        public IActionResult GetAlbums400()
        {
            return _albumService
                .GetAll400()
                .Error<IQueryable<Album>, AlbumSerivceException>(e => new BadRequestObjectResult($"{e.GetType().Name} - {e.Message}"))
                .Or.Error<IQueryable<Album>, ArgumentException>(e => new NotFoundObjectResult($"{e.GetType().Name} - {e.Message}"))
                .Or.Ok(r => new OkObjectResult(r))
                .Result;

            //return _albumService
            //    .GetAll400()
            //    .ResultOrExceptions<IQueryable<Album>, AlbumSerivceException, ArgumentException>(
            //        r => new OkObjectResult(r),
            //        e => new BadRequestObjectResult($"{e.GetType().Name} - {e.Message}"),
            //        e => new NotFoundObjectResult($"{e.GetType().Name} - {e.Message}"));
        }

        [HttpGet("404")]
        public IActionResult GetAlbums404()
        {
            return _albumService
                .GetAll404()
                .ResultOrExceptions<IQueryable<Album>, AlbumSerivceException, ArgumentException>(
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
