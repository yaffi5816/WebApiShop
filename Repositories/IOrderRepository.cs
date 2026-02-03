using Entities;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int id);
    }
}