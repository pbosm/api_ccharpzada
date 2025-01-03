using SystemCcharpzinho.Request.Request;
using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Core.Services.Usuario;
using SystemCcharpzinho.Infrastructure.Repositories.Usuario;

namespace SystemCcharpzinho.Tests

{
    public class LoginTests : BaseTest
    {
        private readonly UsuarioService  _usuarioService;

        public LoginTests()
        {
            var userRepository = new UsuarioRepository(_context);
            _usuarioService = new UsuarioService(userRepository);
        }

        [Fact]
        public async Task RegisterUserTest()
        {
            // Limpar banco de dados antes de cada teste
            _context.Usuarios.RemoveRange(_context.Usuarios);
            _context.SaveChanges();
            
            var userExist = new Usuario { Id = 1, Nome = "New User", Email = "test@example.com", Senha = "password", CPF = "12345678900" };
            await _usuarioService.AddUserAsync(userExist);
            
            var userCreated = new UsuarioCriadoRequest { Nome = "Create user register", Senha = "password123", Email = "registeruser@example.com", CPF = "12345678944" };
            
            var checkExistUser = await _usuarioService.CheckUserByEmailAndCpf(userCreated);
            Assert.False(checkExistUser);
            
            await _usuarioService.AddUserAsync(
                new Usuario
                {
                    Nome  = userCreated.Nome, 
                    Senha = userCreated.Senha, 
                    Email = userCreated.Email, 
                    CPF  = userCreated.CPF 
                    
                }
            );

            var user = await _usuarioService.GetUserByEmailAsync(userCreated.Email);

            Assert.NotNull(user);
            Assert.Equal("Create user register", user.Nome);
        }
    }
}