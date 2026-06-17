using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace Tests
{
    public class OrderRepositoryUnitTests
    {
        [Fact]
        public async Task GetOrderById_ReturnsOrder_WhenIdIsCorrect()
        {
            // Arrange
            var orderId = 10;
            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = orderId,
                    UserId = 1,
                    OrderItems = new List<OrdersItem> { new OrdersItem { OrderItemId = 1, ProductId = 101 } }
                },
                new Order { OrderId = 11, UserId = 2 }
            };

            var mockContext = new Mock<ApiDBContext>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var repository = new OrderRepository(mockContext.Object);

            // Act
            var result = await repository.GetOrderById(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
            Assert.Single(result.OrderItems);
        }
        [Fact]
        public async Task GetOrderById_ReturnsNull_WhenIdIsIncorrect()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { OrderId = 1, UserId = 1 }
            };

            var mockContext = new Mock<ApiDBContext>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var repository = new OrderRepository(mockContext.Object);

            // Act
            var result = await repository.GetOrderById(999);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task AddOrder_SavesOrderWithItems_ReturnsSavedOrder()
        {
            // Arrange
            var mockContext = new Mock<ApiDBContext>();

            var orders = new List<Order>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var repository = new OrderRepository(mockContext.Object);

            var newOrder = new Order
            {
                UserId = 1,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                OrdersSum = 150.5,
                OrderItems = new List<OrdersItem>
                {
                    new OrdersItem { ProductId = 1, Quantity = 2 },
                    new OrdersItem { ProductId = 2, Quantity = 1 }
                }
            };

            // Act
            var result = await repository.AddOrder(newOrder);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.OrderItems.Count);
            mockContext.Verify(x => x.Orders.AddAsync(It.IsAny<Order>(), default), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}
