using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;
namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        string filePath = "..\\WebApiShop\\FileUsers.txt";

        MyShop_216128025Context _myShop_216128025Context;

        public UserRepository(MyShop_216128025Context myShop_216128025Context)
        {
            _myShop_216128025Context = myShop_216128025Context;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _myShop_216128025Context.Users.FindAsync(id);
            return user;
        }
        public async Task<User> AddUser(User user)
        {
            await _myShop_216128025Context.Users.AddAsync(user);
            await _myShop_216128025Context.SaveChangesAsync();
            return user;
        }
        public async Task<User> Login(User loginUser)
        {
            var user = await _myShop_216128025Context.Users.FirstOrDefaultAsync(x=>x.UserName == loginUser.UserName &&
            x.Password== loginUser.Password);
            return user;

        }
        public async void UpdateUser(int id, User user)
        {
            _myShop_216128025Context.Users.Update(user);
            await _myShop_216128025Context.SaveChangesAsync();

        }
    }

}
