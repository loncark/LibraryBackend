using LibraryApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            if (books == null || !books.Any())
            {
                return NotFound("No books found.");
            }

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Invalid request data.");
            }

            var createdBook = await _bookService.CreateBook(book);

            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, [FromBody] Book uBook)
        {
            if (uBook == null || id != uBook.Id)
            {
                return BadRequest("Invalid request data.");
            }

            var updatedBook = await _bookService.UpdateBook(id, uBook);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"Book with ID {id} not found.");
            }
        }

    }
}
