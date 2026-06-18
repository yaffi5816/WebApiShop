using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;
using Xunit;

namespace Tests
{
    public class ProductRepositoryIntegrationTests : IDisposable
    {
        private readonly DatabaseFixture _fixture;
        private readonly ApiDBContext _dbContext;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryIntegrationTests()
        {
            _fixture = new DatabaseFixture();
            _dbContext = _fixture.Context;
            _productRepository = new ProductRepository(_dbContext);
        }
        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public async Task GetProducts_WithComplexFilters_ReturnsExpectedResult()
        {
            // Arrange
            var category = new Category { CategoryName = "Tech" };
            await _dbContext.Categories.AddAsync(category);

            var p1 = new Product { ProductName = "A", ProductDescreption = "Good", Price = 100, Category = category };
            var p2 = new Product { ProductName = "B", ProductDescreption = "Bad", Price = 200, Category = category };
            var p3 = new Product { ProductName = "C", ProductDescreption = "Good", Price = 300, Category = category };

            await _dbContext.Products.AddRangeAsync(p1, p2, p3);
            await _dbContext.SaveChangesAsync();

            // Act
            var (items, totalCount) = await _productRepository.GetProducts(1, 10, new int?[] { category.CategoryId }, "Good", 250, 0);

            // Assert
            Assert.Single(items);
            Assert.Equal("A", items.First().ProductName);
            Assert.Equal(1, totalCount);
        }
        [Fact]
        public async Task GetProducts_Pagination_ReturnsCorrectSliceOfData()
        {
            // Arrange
            var category = new Category { CategoryName = "Hardware" };
            await _dbContext.Categories.AddAsync(category);

            var products = new List<Product>
            {
                new Product { ProductName = "Item 1", Price = 10, Category = category, ProductDescreption = "D" },
                new Product { ProductName = "Item 2", Price = 20, Category = category, ProductDescreption = "D" },
                new Product { ProductName = "Item 3", Price = 30, Category = category, ProductDescreption = "D" },
                new Product { ProductName = "Item 4", Price = 40, Category = category, ProductDescreption = "D" },
                new Product { ProductName = "Item 5", Price = 50, Category = category, ProductDescreption = "D" }
            };
            await _dbContext.Products.AddRangeAsync(products);
            await _dbContext.SaveChangesAsync();

            // Act
            var (items, totalCount) = await _productRepository.GetProducts(
                position: 2,
                skip: 2,
                categoryIds: new int?[] { category.CategoryId },
                description: null,
                maxPrice: null,
                minPrice: null
            );

            // Assert
            Assert.Equal(2, items.Count);
            Assert.Equal(5, totalCount);
            Assert.Equal(30, items[0].Price);
            Assert.Equal(40, items[1].Price);
        }
        [Fact]
        public async Task GetProducts_WhenFiltersMatchNoData_ReturnsEmptyList()
        {
            // Arrange
            var category = new Category { CategoryName = "Tech" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.Products.AddAsync(new Product
            {
                ProductName = "Cheap Phone",
                Price = 100,
                Category = category,
                ProductDescreption = "Old"
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var (items, totalCount) = await _productRepository.GetProducts(
                position: 1,
                skip: 10,
                categoryIds: new int?[] { category.CategoryId },
                description: null,
                maxPrice: null,
                minPrice: 1000
            );

            // Assert
            Assert.NotNull(items);
            Assert.Empty(items);
            Assert.Equal(0, totalCount);
        }
        [Fact]
        public async Task GetProductById_ReturnsProduct_WhenIdExists()
        {
            // Arrange
            var category = new Category { CategoryName = "General" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var product = new Product
            {
                ProductName = "Test Product",
                Price = 100,
                ProductDescreption = "Test",
                CategoryId = category.CategoryId
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetProductById(product.ProductId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.ProductId, result.ProductId);
            Assert.Equal("Test Product", result.ProductName);
            Assert.Equal(100, result.Price);
        }

        [Fact]
        public async Task GetProductById_ReturnsNull_WhenIdDoesNotExist()
        {
            // Act
            var result = await _productRepository.GetProductById(999); // ID שלא קיים

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddProduct_SavesProductToDatabase()
        {
            // Arrange
            var category = new Category { CategoryName = "Hardware" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var newProduct = new Product
            {
                ProductName = "New Keyboard",
                Price = 50,
                ProductDescreption = "Mechanical",
                CategoryId = category.CategoryId
            };


            // Act
            var result = await _productRepository.AddProduct(newProduct);

            // Assert
            Assert.NotEqual(0, result.ProductId);
            var productInDb = await _dbContext.Products.FindAsync(result.ProductId);
            Assert.NotNull(productInDb);
        }

    }
}