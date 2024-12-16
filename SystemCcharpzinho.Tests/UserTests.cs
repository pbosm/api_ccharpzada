using Microsoft.EntityFrameworkCore;
using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Core.Services;
using SystemCcharpzinho.Infrastructure.Context;
using SystemCcharpzinho.Infrastructure.Repositories;

namespace SystemCcharpzinho.Tests

{
    public class UserTests
    {
        private readonly UserService _userService;
        private readonly AppDbContext _context;

        public UserTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);
            
            var userRepository = new UserRepository(_context);
            _userService = new UserService(userRepository);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser()
        {
            var user = new User { Id = 1, Name = "Test User", Email = "test@example.com", Password = "password" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userService.GetUserByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Test User", result.Name);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            var user1 = new User { Id = 1, Name = "User One", Email = "one@example.com", Password = "password" };
            var user2 = new User { Id = 2, Name = "User Two", Email = "two@example.com", Password = "password" };
            _context.Users.AddRange(user1, user2);
            await _context.SaveChangesAsync();

            var result = await _userService.GetAllUsersAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddUserAsync_AddsUser()
        {
            var user = new User { Id = 1, Name = "New User", Email = "new@example.com", Password = "password" };

            await _userService.AddUserAsync(user);
            var result = await _context.Users.FindAsync(1);

            Assert.NotNull(result);
            Assert.Equal("New User", result.Name);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdatesUser()
        {
            var user = new User { Id = 1, Name = "Old Name", Email = "old@example.com", Password = "password" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.Name = "Updated Name";
            await _userService.UpdateUserAsync(user);
            var result = await _context.Users.FindAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
        }

        [Fact]
        public async Task DeleteUserAsync_DeletesUser()
        {
            var user = new User { Id = 1, Name = "User to Delete", Email = "delete@example.com", Password = "password" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _userService.DeleteUserAsync(1);
            var result = await _context.Users.FindAsync(1);

            Assert.Null(result);
        }
    }
}