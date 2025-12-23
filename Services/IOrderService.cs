using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> AddOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int id);
    }
}