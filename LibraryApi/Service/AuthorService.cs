using LibraryApi.Domain;
using LibraryApi.Repository.Interfaces;
using LibraryApi.Service.Interfaces;

namespace LibraryApi.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAllAuthors();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _authorRepository.GetAuthorById(id);
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            return await _authorRepository.CreateAuthor(author);
        }

        public async Task<Author> UpdateAuthor(int id, Author updatedAuthor)
        {
            return await _authorRepository.UpdateAuthor(id, updatedAuthor);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            return await _authorRepository.DeleteAuthor(id);
        }
    }

}
