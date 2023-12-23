using LibraryApi.Domain;
using LibraryApi.Repository;
using LibraryApi.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class AuthorServiceUnitTest
    {
        [Fact]
        public async Task GetAllAuthors_ShouldReturnAllAuthors()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();

            var expectedAuthors = new List<Author>
            {
                new Author { Id = 1, Name = "John", Surname = "Doe" },
                new Author { Id = 2, Name = "Jane", Surname = "Smith" },
            };

            authorRepositoryMock.Setup(repo => repo.GetAllAuthors())
                               .ReturnsAsync(expectedAuthors);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.GetAllAuthors();

            Assert.NotNull(result);
            Assert.Equal(expectedAuthors.Count, ((List<Author>)result).Count);
        }

        [Fact]
        public async Task GetAllAuthors_ShouldReturnEmptyListWhenNoAuthors()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(repo => repo.GetAllAuthors())
                               .ReturnsAsync(new List<Author>());

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.GetAllAuthors();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAuthorById_ShouldReturnAuthorById()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();

            var expectedAuthor = new Author { Id = 1, Name = "John", Surname = "Doe" };
            authorRepositoryMock.Setup(repo => repo.GetAuthorById(It.IsAny<int>()))
                               .ReturnsAsync(expectedAuthor);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.GetAuthorById(1);

            Assert.NotNull(result);
            Assert.Equal(expectedAuthor.Id, result.Id);
        }

        [Fact]
        public async Task GetAuthorById_ShouldReturnNullIfAuthorNotFound()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(repo => repo.GetAuthorById(It.IsAny<int>()))
                               .ReturnsAsync((Author)null);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.GetAuthorById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAuthor_ShouldReturnCreatedAuthor()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();

            var authorToCreate = new Author { Id = 1, Name = "John", Surname = "Doe" };
            authorRepositoryMock.Setup(repo => repo.CreateAuthor(It.IsAny<Author>()))
                               .ReturnsAsync(authorToCreate);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.CreateAuthor(authorToCreate);

            Assert.NotNull(result);
            Assert.Equal(authorToCreate.Id, result.Id);
        }

        [Fact]
        public async Task CreateAuthor_ShouldReturnNullIfCreateFails()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(repo => repo.CreateAuthor(It.IsAny<Author>()))
                               .ReturnsAsync((Author)null);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.CreateAuthor(new Author());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAuthor_ShouldReturnUpdatedAuthor()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();

            var existingAuthor = new Author { Id = 1, Name = "John", Surname = "Doe" };
            authorRepositoryMock.Setup(repo => repo.GetAuthorById(It.IsAny<int>()))
                               .ReturnsAsync(existingAuthor);

            var updatedAuthor = new Author { Id = 1, Name = "UpdatedJohn", Surname = "Doe" };

            authorRepositoryMock.Setup(repo => repo.UpdateAuthor(It.IsAny<int>(), It.IsAny<Author>()))
                               .ReturnsAsync(updatedAuthor);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.UpdateAuthor(1, updatedAuthor);

            Assert.NotNull(result);
            Assert.Equal(updatedAuthor.Name, result.Name);
        }

        [Fact]
        public async Task UpdateAuthor_ShouldReturnNullIfAuthorNotFound()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(repo => repo.GetAuthorById(It.IsAny<int>()))
                               .ReturnsAsync((Author)null);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.UpdateAuthor(1, new Author());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAuthor_ShouldReturnNullIfUpdateFails()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var existingAuthor = new Author { Id = 1, Name = "John", Surname = "Doe" };
            authorRepositoryMock.Setup(repo => repo.GetAuthorById(It.IsAny<int>()))
                               .ReturnsAsync(existingAuthor);

            authorRepositoryMock.Setup(repo => repo.UpdateAuthor(It.IsAny<int>(), It.IsAny<Author>()))
                               .ReturnsAsync((Author)null);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.UpdateAuthor(1, new Author());

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAuthor_ShouldReturnTrueIfAuthorDeleted()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();

            var authorToDelete = new Author { Id = 1, Name = "John", Surname = "Doe" };
            authorRepositoryMock.Setup(repo => repo.DeleteAuthor(It.IsAny<int>()))
                               .ReturnsAsync(true);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.DeleteAuthor(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAuthor_ShouldReturnFalseIfAuthorNotFound()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(repo => repo.DeleteAuthor(It.IsAny<int>()))
                               .ReturnsAsync(false);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.DeleteAuthor(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAuthor_ShouldReturnFalseIfDeleteFails()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(repo => repo.DeleteAuthor(It.IsAny<int>()))
                               .ReturnsAsync(false);

            var authorService = new AuthorService(authorRepositoryMock.Object);

            var result = await authorService.DeleteAuthor(1);

            Assert.False(result);
        }
    }
}

