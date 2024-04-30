using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Queries;
using static Bogus.DataSets.Name;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Spg.AloMalo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosCqsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhotosCqsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // /api/photoscqs?filter=name eq somename&order=lastname asc,firstname asc,id desc
        [HttpGet()]
        public IActionResult GetFiltered([FromQuery] GetPhotosQuery query)
        {
            var result = _mediator.Send(
                new GetPhotosQueryModel(query)
            );
            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePhoto(CreatePhotoCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(
                new CreatePhotoCommand(
                    command.Name,
                    command.Description,
                    ImageTypesDto.Unknown,
                    null,
                    command.Width,
                    command.Height,
                    command.AiGenerated,
                    command.PhotographerId)
                );
                return Created("", result);
            }
            else
            {
                int i = ModelState.ErrorCount;
                return BadRequest(ModelState
                    .Where(m => m.Value?.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                    ));
            }
        }
    }
}
