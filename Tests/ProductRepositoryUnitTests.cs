using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace Tests
{
    public class ProductRepositoryUnitTests
    {
        [Fact]
        public async Task GetProducts_FiltersByDescriptionAndPrice_ReturnsMatchingProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Laptop", ProductDescreption = "High end gaming", Price = 2000, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Mouse", ProductDescreption = "Office mouse", Price = 50, CategoryId = 1 },
                new Product { ProductId = 3, ProductName = "Monitor", ProductDescreption = "4K Display", Price = 500, CategoryId = 2 }
            };

            var mockContext = new Mock<ApiDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            var repository = new ProductRepository(mockContext.Object);

            // Act
            var (items, totalCount) = await repository.GetProducts(1, 10, new int?[] { 1 }, "High", 3000, 1000);

            // Assert
            Assert.Single(items);
            Assert.Equal("Laptop", items.First().ProductName);
            Assert.Equal(1, totalCount);
        }

        [Fact]
        public async Task GetProducts_Pagination_ReturnsCorrectSlice()
        {
            // Arrange
            var products = new List<Product>();
            for (int i = 1; i <= 10; i++)
            {
                products.Add(new Product
                {
                    ProductId = i,
                    ProductName = $"Product {i}",
                    Price = i * 10,
                    CategoryId = 1,
                    ProductDescreption = "Test"
                });
            }

            var mockContext = new Mock<ApiDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            var repository = new ProductRepository(mockContext.Object);

            // Act 
            var (items, totalCount) = await repository.GetProducts(
                position: 2,
                skip: 3,
                categoryIds: Array.Empty<int?>(),
                description: null,
                maxPrice: null,
                minPrice: null
            );

            // Assert
            Assert.Equal(3, items.Count);
            Assert.Equal(10, totalCount);
            Assert.Equal(40, items.First().Price);
            Assert.Equal(60, items.Last().Price);
        }
        [Fact]
        public async Task GetProducts_WhenDatabaseIsEmpty_ReturnsEmptyListAndZeroCount()
        {
            // Arrange
            var emptyList = new List<Product>();
            var mockContext = new Mock<ApiDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(emptyList);

            var repository = new ProductRepository(mockContext.Object);

            // Act
            var (items, totalCount) = await repository.GetProducts(1, 10, Array.Empty<int?>(), null, null, null);

            // Assert
            Assert.NotNull(items);
            Assert.Empty(items);
            Assert.Equal(0, totalCount);
        }

        [Fact]
        public async Task GetProducts_WhenFiltersMatchNothing_ReturnsEmptyListAndZeroCount()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, Price = 100, CategoryId = 1, ProductDescreption = "Expensive Item" }
            };
            var mockContext = new Mock<ApiDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            var repository = new ProductRepository(mockContext.Object);

            // Act 
            var (items, totalCount) = await repository.GetProducts(1, 10, Array.Empty<int?>(), null, 1000, 500);

            // Assert
            Assert.Empty(items);
            Assert.Equal(0, totalCount);
        }
        [Fact]
        public async Task GetProductById_ProductExists_ReturnsProduct()
        {
            // Arrange
            var productId = 10;
            var expectedProduct = new Product { ProductId = productId, ProductName = "Gaming Chair", Price = 1200 };
            var products = new List<Product> { expectedProduct };

            var mockContext = new Mock<ApiDBContext>();

            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            mockContext.Setup(x => x.Products.FindAsync(productId))
                       .ReturnsAsync(expectedProduct);

            var repository = new ProductRepository(mockContext.Object);

            // Act
            var result = await repository.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductId);
        }
        [Fact]
        public async Task GetProductById_ProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Laptop" }
            };

            var mockContext = new Mock<ApiDBContext>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            mockContext.Setup(x => x.Products.FindAsync(99))
                       .ReturnsAsync((Product)null);

            var repository = new ProductRepository(mockContext.Object);

            // Act
            var result = await repository.GetProductById(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddProduct_ValidProduct_ReturnsAddedProductAndCallsSave()
        {
            // Arrange
            var mockContext = new Mock<ApiDBContext>();
            var mockDbSet = new Mock<DbSet<Product>>();
            mockContext.Setup(x => x.Products).Returns(mockDbSet.Object);

            var repository = new ProductRepository(mockContext.Object);
            var newProduct = new Product { ProductId = 4, ProductName = "Keyboard", Price = 100 };

            // Act
            var result = await repository.AddProduct(newProduct);

            // Assert
            mockDbSet.Verify(m => m.AddAsync(It.IsAny<Product>(), default), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
            Assert.Equal(newProduct.ProductName, result.ProductName);
        }

    }
}
