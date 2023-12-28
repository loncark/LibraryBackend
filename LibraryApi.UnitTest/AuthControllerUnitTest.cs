using Azure.Core;
using LibraryApi.Controller;
using LibraryApi.Controllers;
using LibraryApi.Domain;
using LibraryApi.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryApi.UnitTest
{
    public class AuthControllerUnitTest
    {
        private readonly Mock<IUserService> userServiceMock;
        private readonly Mock<IAuthService> authServiceMock;
        private readonly AuthController authController;

        public AuthControllerUnitTest()
        {
            userServiceMock = new Mock<IUserService>();
            authServiceMock = new Mock<IAuthService>();
            authController = new AuthController(userServiceMock.Object, authServiceMock.Object);
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsOkResult()
        {
            var userDto = new UserDto { Username = "testuser", Password = "testpassword" };
            authServiceMock.Setup(x => x.CreatePasswordHash(userDto.Password, out It.Ref<byte[]>.IsAny, out It.Ref<byte[]>.IsAny));

            userServiceMock.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(new User { Username = userDto.Username });

            var result = await authController.Register(userDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var user = Assert.IsType<User>(okResult.Value);
            Assert.Equal(userDto.Username, user.Username);
        }

        [Fact]
        public async Task Login_ValidUser_ReturnsToken()
        {
            var userDto = new UserDto { Username = "testuser", Password = "testpassword" };
            var user = new User { Username = userDto.Username, PasswordHash = new byte[] { 1, 2, 3 }, PasswordSalt = new byte[] { 4, 5, 6 } };
            userServiceMock.Setup(x => x.GetUserByUsernameAsync(userDto.Username)).ReturnsAsync(user);
            authServiceMock.Setup(x => x.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt)).Returns(true);
            authServiceMock.Setup(x => x.CreateToken(user)).Returns("mockedtoken");

            var result = await authController.Login(userDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var token = Assert.IsType<string>(okResult.Value);
            Assert.Equal("mockedtoken", token);
        }

        [Fact]
        public async Task Login_InvalidUser_ReturnsBadRequest()
        {
            var userDto = new UserDto { Username = "testuser", Password = "testpassword" };
            userServiceMock.Setup(x => x.GetUserByUsernameAsync(userDto.Username)).ReturnsAsync((User)null);

            var result = await authController.Login(userDto);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
