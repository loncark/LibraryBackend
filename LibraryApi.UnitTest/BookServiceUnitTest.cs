using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi;
using LibraryApi.Domain;
using LibraryApi.Repository;
using LibraryApi.Service;
using Moq;
using Xunit;

namespace UnitTests
{
    public class BookServiceUnitTest
    {
        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnMappedBooks()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();

            var repositoryBooks = new List<Book>
            {
                new Book { Id = 1, AuthorId = 101, Name = "Book1" },
                new Book { Id = 2, AuthorId = 102, Name = "Book2" },
            };

            bookRepositoryMock.Setup(repo => repo.GetAllBooksAsync())
                              .ReturnsAsync(repositoryBooks);

            var bookService = new BookService(bookRepositoryMock.Object);

            var result = await bookService.GetAllBooksAsync();

            Assert.NotNull(result);
            Assert.Equal(repositoryBooks.Count, result.Count());

            foreach (var book in result)
            {
                Assert.Equal(book.Id, repositoryBooks.Single(b => b.Id == book.Id).Id);
                Assert.Equal(book.AuthorId, repositoryBooks.Single(b => b.Id == book.Id).AuthorId);
                Assert.Equal(book.Name, repositoryBooks.Single(b => b.Id == book.Id).Name);
            }
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnEmptyListWhenNoBooks()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(repo => repo.GetAllBooksAsync())
                              .ReturnsAsync(new List<Book>());

            var bookService = new BookService(bookRepositoryMock.Object);

            var result = await bookService.GetAllBooksAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnNullIfBookNotFound()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync((Book)null);

            var bookService = new BookService(bookRepositoryMock.Object);

            var result = await bookService.GetBookByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnBook()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();

            var expectedBook = new Book { Id = 1, AuthorId = 101, Name = "Book1" };
            bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync(expectedBook);

            var bookService = new BookService(bookRepositoryMock.Object);

            var result = await bookService.GetBookByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(expectedBook, result);
        }

        [Fact]
        public async Task AddBook_ShouldReturnAddedBook()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();

            var bookToAdd = new Book { Id = 1, AuthorId = 101, Name = "Book1" };
            bookRepositoryMock.Setup(repo => repo.CreateBook(It.IsAny<Book>()))
                              .ReturnsAsync(bookToAdd);

            var bookService = new BookService(bookRepositoryMock.Object);

            var result = await bookService.CreateBook(bookToAdd);

            Assert.NotNull(result);
            Assert.Equal(bookToAdd, result);
        }

        [Fact]
        public async Task AddBook_ShouldReturnNullIfAddFails()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(repo => repo.CreateBook(It.IsAny<Book>()))
                              .ReturnsAsync((Book)null);

            var bookService = new BookService(bookRepositoryMock.Object);

            var result = await bookService.CreateBook(new Book());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnUpdatedBook()
        {
            // Arrange
            var bookRepositoryMock = new Mock<IBookRepository>();

            // Mock data to be returned by the repository
            var existingBook = new Book { Id = 1, AuthorId = 101, Name = "Book1" };
            bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync(existingBook);

            var updatedBook = new Book { Id = 1, AuthorId = 101, Name = "UpdatedBook" };

            // Use Task.FromResult to simulate completion and return the updated book
            bookRepositoryMock.Setup(repo => repo.Update(It.IsAny<Book>()))
                              .ReturnsAsync(updatedBook);

            var bookService = new BookService(bookRepositoryMock.Object);

            // Act
            var result = await bookService.UpdateBook(1, updatedBook);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedBook.Name, result.Name);
        }

        [Fact]
        public async Task UpdateBook_InvalidData_ShouldThrowArgumentException()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            var bookService = new BookService(bookRepositoryMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => bookService.UpdateBook(1, null));
        }

        [Fact]
        public async Task UpdateBook_BookNotFound_ShouldThrowFileNotFoundException()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync((Book)null);

            var bookService = new BookService(bookRepositoryMock.Object);

            await Assert.ThrowsAsync<FileNotFoundException>(() => bookService.UpdateBook(1, new Book { Id = 1 }));
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnTrueIfBookDeleted()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();

            var bookToDelete = new Book { Id = 1, AuthorId = 101, Name = "Book1" };
            bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync(bookToDelete);

            bookRepositoryMock.Setup(repo => repo.Remove(It.IsAny<Book>()))
                              .ReturnsAsync(true);

            var bookService = new BookService(bookRepositoryMock.Object);

            var result = await bookService.DeleteBook(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnFalseIfBookNotFound()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync((Book)null);

            var bookService = new BookService(bookRepositoryMock.Object);

            var result = await bookService.DeleteBook(1);

            Assert.False(result);
        }
    }
}
