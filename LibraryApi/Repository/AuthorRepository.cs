using LibraryAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository() { }

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAuthor(int id, Author updatedAuthor)
        {
            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null)
            {
                return null;
            }
            existingAuthor.Name = updatedAuthor.Name;
            existingAuthor.Surname = updatedAuthor.Surname;
            _context.Entry(existingAuthor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return existingAuthor;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return false;
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
