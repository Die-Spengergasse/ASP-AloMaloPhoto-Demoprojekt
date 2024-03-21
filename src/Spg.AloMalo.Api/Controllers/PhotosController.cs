using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Model;

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

        [HttpGet()]
        public IActionResult GetPhoto()
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
                return Created("api/photos/[GUID]", _photoService.Create(command)); // StatusCode 201
            }
            catch (PhotoServiceCreateException)
            {
                return NotFound();
            }
            catch (PhotoServiceValidationException)
            {
                return BadRequest();
            }
            // catch (...)
            // ...
        }

        [HttpPut()]
        public IActionResult UpdatePhoto()
        {
            _photoService.Update(null!);
            return Ok();
        }
    }
}
