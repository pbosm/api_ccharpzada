using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Core.Services.User;
using SystemCcharpzinho.Infrastructure.Repositories.User;
using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.Tests

{
    public class UserTests : BaseTest
    {
        private readonly UserService _userService;

        public UserTests()
        {
            var userRepository = new UserRepository(_context);
            _userService = new UserService(userRepository);
        }

        [Fact]
        public async Task GetUserByIdAsyncTest()
        {
            var user = new User { Id = 1, Nome = "Test User", Email = "test@example.com", Senha = "password", CPF = "12345678900" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userService.GetUserByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("Test User", result.Nome);
        }

        [Fact]
        public async Task GetAllUsersAsyncTest()
        {
            var user1 = new User { Id = 2, Nome = "User One", Email = "one@example.com", Senha = "password", CPF = "12345678900" };
            var user2 = new User { Id = 3, Nome = "User Two", Email = "two@example.com", Senha = "password", CPF = "12345678900"};
            _context.Users.AddRange(user1, user2);
            await _context.SaveChangesAsync();

            var result = await _userService.GetAllUsersAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddUserAsyncTest()
        {
            var user = new User { Id = 4, Nome = "New User", Email = "new@example.com", Senha = "password", CPF = "12345678900" };

            await _userService.AddUserAsync(user);
            var result = await _context.Users.FindAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("New User", result.Nome);
        }

        [Fact]
        public async Task UpdateUserAsyncTest()
        {
            var user = new User { Id = 5, Nome = "Old Name", Email = "old@example.com", Senha = "password", CPF = "12345678900" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _userService.UpdateUserAsync(user.Id, new UserUpdateRequest { Nome = "Updated Name" });
            var result = await _context.Users.FindAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Nome);
        }

        [Fact]
        public async Task DeleteUserAsyncTest()
        {
            var user = new User { Id = 6, Nome = "User to Delete", Email = "delete@example.com", Senha = "password", CPF = "12345678900" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _userService.DeleteUserAsync(user.Id);
            var result = await _context.Users.FindAsync(user.Id);

            Assert.Null(result);
        }
    }
}