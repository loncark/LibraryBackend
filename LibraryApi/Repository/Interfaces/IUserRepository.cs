using LibraryApi.Domain;

namespace LibraryApi.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> CreateUser(User user);
        Task<User> Update(User user);
        Task<bool> Remove(User user);
        Task<bool> SaveChangesAsync();
    }
}
