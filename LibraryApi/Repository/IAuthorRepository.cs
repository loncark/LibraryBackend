using LibraryAPI.Domain;

namespace LibraryAPI.Repository
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> CreateAuthor(Author author);
        Task<Author> UpdateAuthor(int id, Author updatedAuthor);
        Task<bool> DeleteAuthor(int id);
    }

}
