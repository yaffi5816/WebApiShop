using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;
namespace Repositories
{
    public class OrderRepository :  IOrderRepository
    {
        MyShop_216128025Context _myShop_216128025Context;

        public OrderRepository(MyShop_216128025Context myShop_216128025Context)
        {
            _myShop_216128025Context = myShop_216128025Context;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _myShop_216128025Context.Orders.FindAsync(id);
            return order;
        }
        public async Task<Order> AddOrderAsync(Order order)
        {
            await _myShop_216128025Context.Orders.AddAsync(order);
            await _myShop_216128025Context.SaveChangesAsync();
            return order;
        }
    }

}
