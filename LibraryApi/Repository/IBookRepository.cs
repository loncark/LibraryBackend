using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBook(Book book);
        Task<Book> Update(Book book);
        Task<bool> Remove(Book book);
        Task<bool> SaveChangesAsync();
    }
}
