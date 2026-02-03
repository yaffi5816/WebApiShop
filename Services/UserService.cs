using AutoMapper;
using DTOs;
using Entities;
using Repositories;
using System.Text.Json;
namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;// = new UserRepository();
        private readonly IMapper _mapper;
        PasswordService password = new PasswordService();

        public UserService(IUserRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper=mapper;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _repository.GetUserById(id);
        }
        public async Task<User> AddUser(User user)
        {
            if (password.CheckPassword(user.Password).Level < 3)
                return null;
            return await _repository.AddUser(user);
        }
        public async Task<User> Login(User user)
        {
            return await _repository.Login(user);
        }
        public void UpdateUser(int id, User user)
        {
            _repository.UpdateUser(id, user);
        }


        public async Task<IEnumerable<UserDTO>> GetAsync()
        {
            IEnumerable<User> users = await _repository.GetAsync();
            IEnumerable<UserDTO> usersDTO = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
            return usersDTO;
        }




    }
}
