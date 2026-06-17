using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using Xunit;

namespace Tests

{
    public class CategoryRepositoryUnitTests
    {
        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "KPIs" },
            new Category { CategoryId = 2, CategoryName = "Charts" }
        };

            var mockContext = new Mock<ApiDBContext>();
            mockContext
                .Setup(x => x.Categories)
                .ReturnsDbSet(categories);

            var categoryRepository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await categoryRepository.GetCategories();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCategories_WhenEmpty_ReturnsNoItems()
        {
            // Arrange
            var categories = new List<Category>(); // Empty list

            var mockContext = new Mock<ApiDBContext>();
            mockContext
                .Setup(x => x.Categories)
                .ReturnsDbSet(categories);

            var categoryRepository = new CategoryRepository(mockContext.Object);

            // Act
            var result = await categoryRepository.GetCategories();

            // Assert
            Assert.Empty(result);
            Assert.NotNull(result);
        }
    }
}
