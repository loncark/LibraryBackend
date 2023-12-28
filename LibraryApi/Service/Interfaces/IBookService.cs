namespace LibraryApi.Service.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBook(Book book);
        Task<Book> UpdateBook(int id, Book updatedBook);
        Task<bool> DeleteBook(int id);
    }

}
