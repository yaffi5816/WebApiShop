using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUserById(int id);
        Task<User> Login(User user);
        void UpdateUser(int id, User user);
        Task<IEnumerable<User>> GetAsync();
    }
}