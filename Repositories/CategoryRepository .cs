using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;
namespace Repositories
{
    public class CategoryRepository :  ICategoryRepository
    {

        MyShop_216128025Context _myShop_216128025Context;

        public CategoryRepository(MyShop_216128025Context myShop_216128025Context)
        {
            _myShop_216128025Context = myShop_216128025Context;
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _myShop_216128025Context.Categories.ToListAsync();
        }
    }

}
