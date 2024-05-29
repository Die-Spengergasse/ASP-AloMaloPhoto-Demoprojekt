﻿using Microsoft.AspNetCore.Mvc;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Queries;
using System.Net.Http.Headers;

namespace Spg.AloMalo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        // GET:  Liefert alle Produkte
        // http://www.myservice.at/api/products
        // POST: Legt neues produkt an
        // http://www.myservice.at/api/products             (+ Content im Body)
        // DELETE: Löscht alle Produkte
        // http://www.myservice.at/api/products
        // GET: Liefert ein Produkt
        // http://www.myservice.at/api/products/[GUID]
        // DELETE: Löscht ein Produkt
        // http://www.myservice.at/api/products/[GUID]
        // PUT: Aktualisiert alle Produkte
        // http://www.myservice.at/api/products             (+ Content im Body)
        // PUT: Aktualisiert ein Produkt
        // http://www.myservice.at/api/products/[GUID]      (+ Content im Body)
        // PATCH: Aktualisiert einen Teil aller Produkte
        // http://www.myservice.at/api/products             (+ Content im Body)
        // PATCH: Aktualisiert einen Teil eines Produkts
        // http://www.myservice.at/api/products/[GUID]      (+ Content im Body)

        // GET: Liefert alle Warenkörbe eines Kunden
        // http://www.myservice.at/api/customer/[GUID]/shoppingcarts
        // GET: Liefert einen bestimmten Warenkorb eines Kunden
        // http://www.myservice.at/api/customer/[GUID]/shoppingcarts/[GUID]

        // GET: Liefert Produkte gefiltert nach Namensteil
        // http://www.myservice.at/api/products?name_contains=weihnacht&state=verfuegbar&.........

        // GET: Liefert alle Produkte in einem Warenkorb eines Kunden
        // http://www.myservice.at/api/schoppingcart/[Customer-GUID]/products?.....

        // URI
        // http://www.myservice.at/api/products?name_contains=weihnacht&state=verfuegbar&.........
        // <---------URL---------><---PATH----><---------------QUERY(String)-----------------------
        //                        <-----------------------------URN--------------------------------


        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        // https://code-maze.com/upload-files-dot-net-core-angular/
        [HttpPost("upload"), DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue
                        .Parse(file.ContentDisposition)
                        .FileName
                        ?.Trim('"') 
                            ?? "";
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet()]
        public IActionResult GetPhotos()
        {
            // Deswegen Rich Typed IDs
            //var r = _photoService.GetWhatEver(new PhotoId(123), new AlbumId(456), new PhotographerId(147));

            IQueryable<PhotoDto> result = _photoService.GetPhotos();
            return Ok(result);  // StatusCode 200, 204 (NoContent)
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePhoto(CreatePhotoCommand command)
        {
            // Try-Catch-Blöcke im Controller sind richtig HÄSSLICH!
            // (Lösung im AlbumController)
            try
            {
                PhotoDto dto = _photoService.Create(command);
                return Created($"api/photos/{dto.Id}", dto); // StatusCode 201
            }
            catch (PhotoServiceCreateException)
            {
                return NotFound();
            }
            catch (PhotoServiceValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            // catch (...)
            // ...
        }

        [HttpPut()]
        public IActionResult UpdatePhoto()
        {
            _photoService.Update(null!); // TODO: Implementation
            return Ok();
        }

        [HttpDelete()]
        public IActionResult DeletePhoto()
        {
            //_photoService.Delete(null!); // TODO: Implementation
            return Ok();
        }
        //*[HttpGet()]
        public IActionResult Get([FromQuery()] GetPhotosQueryHandler query)
        {
            List<PhotoDto> data = _photoService
                .Send(new GetPhotosQueryModel(query))
                .Result.ToList();
        }


    }
}
