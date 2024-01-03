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
        public async Task<IActionResult> GetAll()
        {
            var books = await _services.BookServices.GetAllBooksAsync(false);
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBook([FromRoute(Name = "id")] int id)
        {           
            var book = await _services.BookServices.GetBookByIdAsync(id, false);       
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDtoForInsertion bookDto)
        {
            if (bookDto is null) return BadRequest();
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(bookDto);
            }
            var book = await _services.BookServices.CreateOneBookAsync(bookDto);
            return StatusCode(201, book);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _services.BookServices.UpdateBookAsync(bookDto, id, false);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute(Name = "id")] int id)
        {
            await _services.BookServices.DeleteOneBookAsync(id, false);
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartialyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookpatch)
        {
            if (bookpatch is null)
            {
                return BadRequest();
            }
            var result = await _services.BookServices.GetOneBookForPatchAsync(id, false);   
            bookpatch.ApplyTo(result.bookDtoForUpdate,ModelState);
            TryValidateModel(result.bookDtoForUpdate);
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
           await _services.BookServices.SaveChangesForPachAsync(result.bookDtoForUpdate,result.book);
            return NoContent();
        }
    }
}

