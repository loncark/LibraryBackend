using LibraryApi.Domain;

namespace LibraryApi.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(int id, User updatedUser);
        Task<bool> DeleteUser(int id);
    }

}
