using Entities;
using Repositories;
using System.Text.Json;
namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _repository.GetOrderByIdAsync(id);
        }
        public async Task<Order> AddOrderAsync(Order order)
        {
            return await _repository.AddOrderAsync(order);
        }


    }
}
