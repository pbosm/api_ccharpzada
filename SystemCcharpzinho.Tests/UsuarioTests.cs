using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Core.Services.Usuario;
using SystemCcharpzinho.Infrastructure.Repositories.Usuario;
using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.Tests

{
    public class UsuarioTests : BaseTest
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioTests()
        {
            var userRepository = new UsuarioRepository(_context);
            _usuarioService = new UsuarioService(userRepository);
        }

        [Fact]
        public async Task GetUserByIdAsyncTest()
        {
            var user = new Usuario { Id = 1, Nome = "Test User", Email = "test@example.com", Senha = "password", CPF = "12345678900" };
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            var result = await _usuarioService.GetUserByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("Test User", result.Nome);
        }

        [Fact]
        public async Task GetAllUsersAsyncTest()
        {
            var user1 = new Usuario { Id = 2, Nome = "User One", Email = "one@example.com", Senha = "password", CPF = "12345678900" };
            var user2 = new Usuario { Id = 3, Nome = "User Two", Email = "two@example.com", Senha = "password", CPF = "12345678900"};
            _context.Usuarios.AddRange(user1, user2);
            await _context.SaveChangesAsync();

            var result = await _usuarioService.GetAllUsersAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddUserAsyncTest()
        {
            var user = new Usuario { Id = 4, Nome = "New User", Email = "new@example.com", Senha = "password", CPF = "12345678900" };

            await _usuarioService.AddUserAsync(user);
            var result = await _context.Usuarios.FindAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("New User", result.Nome);
        }

        [Fact]
        public async Task UpdateUserAsyncTest()
        {
            var user = new Usuario { Id = 5, Nome = "Old Name", Email = "old@example.com", Senha = "password", CPF = "12345678900" };
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            await _usuarioService.UpdateUserAsync(user.Id, new UsuarioAtulizadoRequest { Nome = "Updated Name" });
            var result = await _context.Usuarios.FindAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Nome);
        }

        [Fact]
        public async Task DeleteUserAsyncTest()
        {
            var user = new Usuario { Id = 6, Nome = "User to Delete", Email = "delete@example.com", Senha = "password", CPF = "12345678900" };
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            await _usuarioService.DeleteUserAsync(user.Id);
            var result = await _context.Usuarios.FindAsync(user.Id);

            Assert.Null(result);
        }
    }
}