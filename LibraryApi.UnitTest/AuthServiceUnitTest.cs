using LibraryApi.Domain;
using LibraryApi.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.UnitTest
{
    public class AuthServiceUnitTest
    {
        private readonly IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        /*
        [Fact]
        public void CreateToken_ReturnsValidToken()
        {
            var authService = new AuthService(_configuration);
            var user = new User { Id = 1, Username = "testuser" };

            var token = authService.CreateToken(user);

            Assert.NotNull(token);
            Assert.NotEmpty(token);

            var jwtHandler = new JwtSecurityTokenHandler();
            var parsedToken = jwtHandler.ReadToken(token) as JwtSecurityToken;

            Assert.NotNull(parsedToken);
            Assert.True(parsedToken.ValidTo > DateTime.UtcNow);
            Assert.Contains(parsedToken.Claims, claim => claim.Type == ClaimTypes.Name && claim.Value == user.Username);
            Assert.Contains(parsedToken.Claims, claim => claim.Type == ClaimTypes.Role && claim.Value == "Admin");
        }*/

        [Fact]
        public void CreatePasswordHash_ReturnsValidHashAndSalt()
        {
            var authService = new AuthService(_configuration);
            var password = "testpassword";

            authService.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            Assert.NotNull(passwordHash);
            Assert.NotEmpty(passwordHash);
            Assert.NotNull(passwordSalt);
            Assert.NotEmpty(passwordSalt);

            Assert.True(authService.VerifyPasswordHash(password, passwordHash, passwordSalt));
        }

        [Fact]
        public void VerifyPasswordHash_ReturnsTrueForValidPassword()
        {
            var authService = new AuthService(_configuration);
            var password = "testpassword";
            authService.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            var result = authService.VerifyPasswordHash(password, passwordHash, passwordSalt);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPasswordHash_ReturnsFalseForInvalidPassword()
        {
            var authService = new AuthService(_configuration);
            var password = "testpassword";
            authService.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            var result = authService.VerifyPasswordHash("invalidpassword", passwordHash, passwordSalt);

            Assert.False(result);
        }
    }
}
