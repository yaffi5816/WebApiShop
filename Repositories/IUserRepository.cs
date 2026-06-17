using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User newUser);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task UpdateUser(int id, User updateUser);
        Task<bool> UserWithSameEmail(string email, int id);
    }
}