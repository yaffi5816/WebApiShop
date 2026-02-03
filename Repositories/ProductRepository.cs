using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;
namespace Repositories
{
    public class ProductRepository : IProductRepository
    {

        MyShop_216128025Context _myShop_216128025Context;

        public ProductRepository(MyShop_216128025Context myShop_216128025Context)
        {
            _myShop_216128025Context = myShop_216128025Context;
        }

        public async Task<IEnumerable<Product>> GetAsync(int position, int skip, string? name, int? minPrice, int? maxPrice, int?[] categoriesId)
        {
            return await _myShop_216128025Context.Products.ToListAsync();
        }
    }

}
