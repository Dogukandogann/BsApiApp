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
        public IActionResult CreateBook([FromBody] Book book)
        {
            if (book is null) return BadRequest();
            _services.BookServices.CreateOneBook(book);
            return StatusCode(201, book);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            _services.BookServices.UpdateBook(bookDto, id, true);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
        {
            _services.BookServices.DeleteOneBook(id, false);
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartialyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookpatch)
        {
            var book = _services.BookServices.GetBookById(id, true);           
            bookpatch.ApplyTo(book);
            _services.BookServices.UpdateBook(new BookDtoForUpdate(book.Id,book.Title,book.Price), id, true);
            return NoContent();
        }
    }
}

