using LibraryApi.Domain;
using LibraryApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controller
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
    }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User();
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var createdUser = await _userService.CreateUser(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await _userService.GetUserByUsernameAsync(request.Username);

            if (user == null || !_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = _authService.CreateToken(user);
            return Ok(token);
        }
    }
}
