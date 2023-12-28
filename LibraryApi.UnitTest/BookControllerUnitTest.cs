using LibraryApi.Controllers;
using LibraryApi;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApi.Service.Interfaces;

namespace UnitTests
{
    public class BookControllerUnitTest
    {
        [Fact]
        public async Task GetAllBooks_ReturnsOkResultWithBooks()
        {
            var bookServiceMock = new Mock<IBookService>();
            var books = new List<Book> { new Book { Id = 1, Name = "Book1" }, new Book { Id = 2, Name = "Book2" } };
            bookServiceMock.Setup(service => service.GetAllBooksAsync()).ReturnsAsync(books);
            var controller = new BookController(bookServiceMock.Object);

            var actionResult = await controller.GetAllBooks();

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedBooks = Assert.IsAssignableFrom<IEnumerable<Book>>(okObjectResult.Value);
            Assert.Equal(2, returnedBooks.Count());
        }

        [Fact]
        public async Task GetAllBooks_ReturnsNotFoundResultWhenNoBooks()
        {
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(service => service.GetAllBooksAsync()).ReturnsAsync(new List<Book>());
            var controller = new BookController(bookServiceMock.Object);

            var actionResult = await controller.GetAllBooks();

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("No books found.", notFoundObjectResult.Value);
        }

        [Fact]
        public async Task GetBookById_ReturnsOkResultWithBook()
        {
            var bookServiceMock = new Mock<IBookService>();
            var book = new Book { Id = 1, Name = "Book1" };
            bookServiceMock.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync(book);
            var controller = new BookController(bookServiceMock.Object);

            var actionResult = await controller.GetBookById(1);

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedBook = Assert.IsType<Book>(okObjectResult.Value);
            Assert.Equal(1, returnedBook.Id);
            Assert.Equal("Book1", returnedBook.Name);
        }

        [Fact]
        public async Task GetBookById_ReturnsNotFoundResultWhenBookNotFound()
        {
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync((Book)null);
            var controller = new BookController(bookServiceMock.Object);

            var actionResult = await controller.GetBookById(1);

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("Book with ID 1 not found.", notFoundObjectResult.Value);
        }

        [Fact]
        public async Task CreateBook_ReturnsCreatedResponse()
        {
            var mockBookService = new Mock<IBookService>();
            var controller = new BookController(mockBookService.Object);
            var newBook = new Book { Id = 1, Name = "Test Book", AuthorId = 1 };

            mockBookService.Setup(repo => repo.CreateBook(newBook))
                .ReturnsAsync(newBook);

            var result = await controller.CreateBook(newBook);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsAssignableFrom<Book>(createdAtActionResult.Value);
            Assert.Equal(newBook.Id, model.Id);
        }

        [Fact]
        public async Task CreateBook_ReturnsBadRequestResultWhenBookIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var controller = new BookController(bookServiceMock.Object);

            var actionResult = await controller.CreateBook(null);

            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Invalid request data.", badRequestObjectResult.Value);
        }

        [Fact]
        public async Task UpdateBook_ReturnsOkResultWithUpdatedBook()
        {
            var bookServiceMock = new Mock<IBookService>();
            var updatedBook = new Book { Id = 1, Name = "UpdatedBook" };
            bookServiceMock.Setup(service => service.UpdateBook(1, It.IsAny<Book>())).ReturnsAsync(updatedBook);
            var controller = new BookController(bookServiceMock.Object);

            var actionResult = await controller.UpdateBook(1, updatedBook);

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedBook = Assert.IsType<Book>(okObjectResult.Value);
            Assert.Equal(1, returnedBook.Id);
            Assert.Equal("UpdatedBook", returnedBook.Name);
        }

        [Fact]
        public async Task UpdateBook_ReturnsBadRequestResultWhenBookIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var controller = new BookController(bookServiceMock.Object);

            var actionResult = await controller.UpdateBook(1, null);

            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Invalid request data.", badRequestObjectResult.Value);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNoContentResult()
        {
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(service => service.DeleteBook(1)).ReturnsAsync(true);
            var controller = new BookController(bookServiceMock.Object);

            var actionResult = await controller.DeleteBook(1);

            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFoundResultWhenBookNotFound()
        {
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(service => service.DeleteBook(1)).ReturnsAsync(false);
            var controller = new BookController(bookServiceMock.Object);

            var actionResult = await controller.DeleteBook(1);

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("Book with ID 1 not found.", notFoundObjectResult.Value);
        }

    }
}
