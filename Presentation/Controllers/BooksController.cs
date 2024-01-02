using Entities.DataTransferObjects;
using Entities.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using static Entities.Exceptions.NotFoundException;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _services;

        public BooksController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _services.BookServices.GetAllBooks(false);
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {           
            var book = _services.BookServices.GetBookById(id, false);       
            return Ok(book);
        }
        [HttpPost]
        public IActionResult CreateBook([FromBody] BookDtoForInsertion bookDto)
        {
            if (bookDto is null) return BadRequest();
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(bookDto);
            }
            var book = _services.BookServices.CreateOneBook(bookDto);
            return StatusCode(201, book);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            _services.BookServices.UpdateBook(bookDto, id, false);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
        {
            _services.BookServices.DeleteOneBook(id, false);
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartialyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookpatch)
        {
            if (bookpatch is null)
            {
                return BadRequest();
            }
            var result = _services.BookServices.GetOneBookForPatch(id, false);   
            bookpatch.ApplyTo(result.bookDtoForUpdate,ModelState);
            TryValidateModel(result.bookDtoForUpdate);
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            _services.BookServices.SaveChangesForPach(result.bookDtoForUpdate,result.book);
            return NoContent();
        }
    }
}

