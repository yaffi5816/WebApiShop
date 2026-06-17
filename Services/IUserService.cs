using DTO;

namespace Services
{
    public interface IUserService
    {
        Task<AuthResultDTO> AddUser(PostUserDTO user);
        Task<UserDTO> GetUserById(int id);
        Task<IEnumerable<UserDTO>> GetUsers();
        bool IsPasswordStrong(string password);
        Task<AuthResultDTO> Login(LoginUserDTO loginUser);
        Task UpdateUser(int id, PostUserDTO user);
        Task<bool> UserWithSameEmail(string email, int id = -1);
    }
}