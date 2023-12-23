using LibraryApi.Domain;
using LibraryApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthors();
            if (authors == null || !authors.Any())
            {
                return NotFound("No authors found.");
            }
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound($"Author with ID {id} not found.");
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] Author author)
        {
            var createdAuthor = await _authorService.CreateAuthor(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> UpdateAuthor(int id, [FromBody] Author updatedAuthor)
        {
            var result = await _authorService.UpdateAuthor(id, updatedAuthor);
            if (result == null)
            {
                return NotFound($"Author with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var success = await _authorService.DeleteAuthor(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound($"Author with ID {id} not found.");
        }
    }

}
