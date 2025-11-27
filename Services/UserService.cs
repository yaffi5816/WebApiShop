using Entities;
using Repositories;
using System.Text.Json;
namespace Services
{
    public class UserService
    {
        UserRepository repository=new UserRepository();
        PasswordService password=new PasswordService();

        public User GetUserById(int id)
        {
            return repository.GetUserById(id);
        }
        public User AddUser(User user)
        {
            return repository.AddUser(user);
        }
        public User Login(User user)
        {
            return repository.Login(user);
        }
        public void UpdateUser(int id, User user)
        {
            repository.UpdateUser(id,user);
        }



    }
}
