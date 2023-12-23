using LibraryApi.Repository;

namespace LibraryApi.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService() { }

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = await _repository.GetAllBooksAsync();

            return books.Select(book => new Book
            {
                Id = book.Id,
                AuthorId = book.AuthorId,
                Name = book.Name
            });
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _repository.GetBookByIdAsync(id);
        }

        public async Task<Book> CreateBook(Book book)
        {
            return await _repository.CreateBook(book);
        }

        public async Task<Book> UpdateBook(int id, Book updatedBook)
        {
            if (updatedBook == null || id != updatedBook.Id)
            {
                throw new ArgumentException("Invalid request data.");
            }

            var existingBook = await _repository.GetBookByIdAsync(id);

            if (existingBook == null)
            {
                throw new FileNotFoundException($"Book with ID {id} not found.");
            }

            existingBook.Name = updatedBook.Name;

            await _repository.Update(existingBook);

            await _repository.SaveChangesAsync();

            return existingBook;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _repository.GetBookByIdAsync(id);

            if (book == null)
            {
                return false;
            }

            await _repository.Remove(book);
            await _repository.SaveChangesAsync();

            return true;
        }
    }

}
