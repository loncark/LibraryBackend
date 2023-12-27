using LibraryApi.Domain;
using LibraryApi.Repository;

namespace LibraryApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService() { }

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllUsersAsync();

            return users.Select(user => new User
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            });
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetUserByIdAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _repository.GetUserByUsernameAsync(username);
        }

        public async Task<User> CreateUser(User user)
        {
            return await _repository.CreateUser(user);
        }

        public async Task<User> UpdateUser(int id, User updatedUser)
        {
            if (updatedUser == null || id != updatedUser.Id)
            {
                throw new ArgumentException("Invalid request data.");
            }

            var existingUser = await _repository.GetUserByIdAsync(id);

            if (existingUser == null)
            {
                throw new FileNotFoundException($"User with ID {id} not found.");
            }

            existingUser.Username = updatedUser.Username;

            await _repository.Update(existingUser);

            await _repository.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            await _repository.Remove(user);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
