using LibraryAPI.Domain;

namespace LibraryAPI.Service
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> CreateAuthor(Author author);
        Task<Author> UpdateAuthor(int id, Author updatedAuthor);
        Task<bool> DeleteAuthor(int id);
    }

}
