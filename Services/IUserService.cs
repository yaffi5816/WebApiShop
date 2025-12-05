using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<User> GetUserById(int id);
        Task<User> Login(User user);
        void UpdateUser(int id, User user);
    }
}