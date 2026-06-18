using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;
using Xunit;

namespace Tests
{
    public class UserRepositoryIntegrationTests : IDisposable
    {
        private readonly DatabaseFixture _fixture;
        private readonly ApiDBContext _dbContext;
        private readonly UserRepository _userRepository;
        private readonly IPasswordService _passwordService = new PasswordService();

        public UserRepositoryIntegrationTests()
        {
            _fixture = new DatabaseFixture();
            _dbContext = _fixture.Context;
            _userRepository = new UserRepository(_dbContext);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsNull_WhenEmailDoesNotExist()
        {
            // Arrange
            var hashedPassword = _passwordService.HashPassword("CorrectPassword");
            var user = new User { FirstName = "RealUser", UserName = "real@test.com", Password = hashedPassword };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetUserByEmail("nonexistent@test.com");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUsers_ReturnsEmptyList_WhenNoUsersInDatabase()
        {
            // Act
            var result = await _userRepository.GetUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUsers_ReturnsUsersWithOrders_FromDatabase()
        {
            // Arrange
            var user = new User { FirstName = "Alice", UserName = "alice@db.com", Password = "123" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var order = new Order { UserId = user.UserId, OrderDate = DateOnly.FromDateTime(DateTime.Now), OrdersSum = 99.9 };
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetUsers();

            // Assert
            Assert.NotEmpty(result);
            var fetchedUser = result.First(u => u.UserName == "alice@db.com");
            Assert.Single(fetchedUser.Orders);
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsCorrectUser_WhenEmailMatches()
        {
            // Arrange
            var hashedPassword = _passwordService.HashPassword("SecretPassword");
            var user = new User { FirstName = "LoginTest", UserName = "login@test.com", Password = hashedPassword };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetUserByEmail("login@test.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("LoginTest", result.FirstName);
            Assert.True(_passwordService.VerifyPassword("SecretPassword", result.Password));
        }

        [Fact]
        public async Task UpdateUser_ActuallyPersistsChangesInDatabase()
        {
            // Arrange
            var user = new User { FirstName = "Before", UserName = "before@test.com", Password = "123" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            _dbContext.Entry(user).State = EntityState.Detached;

            var userToUpdate = new User
            {
                UserId = user.UserId,
                FirstName = "After",
                UserName = "after@test.com",
                Password = "123"
            };

            // Act
            await _userRepository.UpdateUser(user.UserId, userToUpdate);

            // Assert
            _dbContext.ChangeTracker.Clear();
            var updatedUser = await _dbContext.Users.FindAsync(user.UserId);

            Assert.NotNull(updatedUser);
            Assert.Equal("After", updatedUser.FirstName);
            Assert.Equal("after@test.com", updatedUser.UserName);
        }

        [Fact]
        public async Task UserWithSameEmail_CorrectlyIdentifiesDuplicates()
        {
            // Arrange
            var existingUser = new User { FirstName = "Existing", UserName = "taken@test.com", Password = "123" };
            await _dbContext.Users.AddAsync(existingUser);
            await _dbContext.SaveChangesAsync();

            // Act
            var isAvailableForNew = await _userRepository.UserWithSameEmail("taken@test.com", -1);
            var isAvailableForSelf = await _userRepository.UserWithSameEmail("taken@test.com", existingUser.UserId);

            // Assert
            Assert.False(isAvailableForNew);
            Assert.True(isAvailableForSelf);
        }

        [Fact]
        public async Task AddUser_SavesUserCorrectly_AndGeneratesId()
        {
            // Arrange
            var newUser = new User
            {
                FirstName = "New",
                LastName = "User",
                UserName = "new@test.com",
                Password = "Password123"
            };

            // Act
            var result = await _userRepository.AddUser(newUser);

            // Assert
            Assert.NotEqual(0, result.UserId);
            Assert.Equal("New", result.FirstName);

            var userInDb = await _dbContext.Users.FindAsync(result.UserId);
            Assert.NotNull(userInDb);
            Assert.Equal("new@test.com", userInDb.UserName);
        }

        [Fact]
        public async Task GetUserById_ReturnsUser_WhenIdIsCorrect()
        {
            // Arrange
            var user = new User
            {
                FirstName = "Test",
                LastName = "User",
                UserName = "findme@test.com",
                Password = "123"
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetUserById(user.UserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserId, result.UserId);
            Assert.Equal("findme@test.com", result.UserName);
        }

        [Fact]
        public async Task GetUserById_ReturnsNull_WhenIdIsIncorrect()
        {
            // Arrange
            var user = new User { FirstName = "Exist", UserName = "exist@test.com", Password = "123" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var nonExistentId = 9999;

            // Act
            var result = await _userRepository.GetUserById(nonExistentId);

            // Assert
            Assert.Null(result);
        }
    }
}