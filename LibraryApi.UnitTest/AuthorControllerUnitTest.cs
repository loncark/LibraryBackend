
using LibraryApi.Controllers;
using LibraryApi.Domain;
using LibraryApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    public class AuthorControllerUnitTest
    {
        [Fact]
        public async Task GetAllAuthors_ReturnsOkResultWithAuthors()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var authors = new List<Author> { new Author { Id = 1, Name = "Author1" }, new Author { Id = 2, Name = "Author2" } };
            authorServiceMock.Setup(service => service.GetAllAuthors()).ReturnsAsync(authors);
            var controller = new AuthorController(authorServiceMock.Object);

            var actionResult = await controller.GetAllAuthors();

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedAuthors = Assert.IsAssignableFrom<IEnumerable<Author>>(okObjectResult.Value);
            Assert.Equal(2, returnedAuthors.Count());
        }

        [Fact]
        public async Task GetAllAuthors_ReturnsNotFoundResultWhenNoAuthors()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(service => service.GetAllAuthors()).ReturnsAsync(new List<Author>());
            var controller = new AuthorController(authorServiceMock.Object);

            var actionResult = await controller.GetAllAuthors();

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("No authors found.", notFoundObjectResult.Value);
        }

        [Fact]
        public async Task GetAuthorById_ReturnsOkResultWithAuthor()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var author = new Author { Id = 1, Name = "Author1" };
            authorServiceMock.Setup(service => service.GetAuthorById(1)).ReturnsAsync(author);
            var controller = new AuthorController(authorServiceMock.Object);

            var actionResult = await controller.GetAuthorById(1);

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedAuthor = Assert.IsType<Author>(okObjectResult.Value);
            Assert.Equal(1, returnedAuthor.Id);
            Assert.Equal("Author1", returnedAuthor.Name);
        }

        [Fact]
        public async Task GetAuthorById_ReturnsNotFoundResultWhenAuthorNotFound()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(service => service.GetAuthorById(1)).ReturnsAsync((Author)null);
            var controller = new AuthorController(authorServiceMock.Object);

            var actionResult = await controller.GetAuthorById(1);

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("Author with ID 1 not found.", notFoundObjectResult.Value);
        }

        [Fact]
        public async Task CreateAuthor_ReturnsCreatedAtActionResultWithAuthor()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var newAuthor = new Author { Id = 1, Name = "NewAuthor" };
            authorServiceMock.Setup(service => service.CreateAuthor(It.IsAny<Author>())).ReturnsAsync(newAuthor);
            var controller = new AuthorController(authorServiceMock.Object);

            var actionResult = await controller.CreateAuthor(newAuthor);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnedAuthor = Assert.IsType<Author>(createdAtActionResult.Value);
            Assert.Equal(1, returnedAuthor.Id);
            Assert.Equal("NewAuthor", returnedAuthor.Name);
        }

        [Fact]
        public async Task CreateAuthor_ReturnsInternalServerErrorWhenAuthorCreationFails()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(service => service.CreateAuthor(It.IsAny<Author>()))
                .Throws(new Exception("Author creation failed."));
            var controller = new AuthorController(authorServiceMock.Object);

            await Assert.ThrowsAsync<Exception>(async () => await controller.CreateAuthor(new Author()));
        }

        [Fact]
        public async Task UpdateAuthor_ReturnsOkResultWithUpdatedAuthor()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var updatedAuthor = new Author { Id = 1, Name = "UpdatedAuthor" };
            authorServiceMock.Setup(service => service.UpdateAuthor(1, It.IsAny<Author>())).ReturnsAsync(updatedAuthor);
            var controller = new AuthorController(authorServiceMock.Object);

            var actionResult = await controller.UpdateAuthor(1, updatedAuthor);

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedAuthor = Assert.IsType<Author>(okObjectResult.Value);
            Assert.Equal(1, returnedAuthor.Id);
            Assert.Equal("UpdatedAuthor", returnedAuthor.Name);
        }

        [Fact]
        public async Task UpdateAuthor_ReturnsNotFoundResultWhenAuthorNotFound()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(service => service.UpdateAuthor(1, It.IsAny<Author>())).ReturnsAsync((Author)null);
            var controller = new AuthorController(authorServiceMock.Object);

            var actionResult = await controller.UpdateAuthor(1, new Author());

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("Author with ID 1 not found.", notFoundObjectResult.Value);
        }

        [Fact]
        public async Task DeleteAuthor_ReturnsNoContentResult()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(service => service.DeleteAuthor(1)).ReturnsAsync(true);
            var controller = new AuthorController(authorServiceMock.Object);

            var actionResult = await controller.DeleteAuthor(1);

            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task DeleteAuthor_ReturnsNotFoundResultWhenAuthorNotFound()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(service => service.DeleteAuthor(1)).ReturnsAsync(false);
            var controller = new AuthorController(authorServiceMock.Object);

            var actionResult = await controller.DeleteAuthor(1);

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("Author with ID 1 not found.", notFoundObjectResult.Value);
        }

    }
}
