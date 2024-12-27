using SystemCcharpzinho.Request.Request;
using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Core.Services.User;
using SystemCcharpzinho.Infrastructure.Repositories.User;

namespace SystemCcharpzinho.Tests

{
    public class LoginTests : BaseTest
    {
        private readonly UserService  _userService;

        public LoginTests()
        {
            var userRepository = new UserRepository(_context);
            _userService = new UserService(userRepository);
        }

        [Fact]
        public async Task RegisterUserTest()
        {
            // Limpar banco de dados antes de cada teste
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();
            
            var userExist = new User { Id = 1, Nome = "New User", Email = "test@example.com", Senha = "password", CPF = "12345678900" };
            await _userService.AddUserAsync(userExist);
            
            var userCreated = new UserCreatedRequest { Nome = "Create user register", Senha = "password123", Email = "registeruser@example.com", CPF = "12345678944" };
            
            var checkExistUser = await _userService.CheckUserByEmailAndCpf(userCreated);
            Assert.False(checkExistUser);
            
            await _userService.AddUserAsync(
                new User
                {
                    Nome  = userCreated.Nome, 
                    Senha = userCreated.Senha, 
                    Email = userCreated.Email, 
                    CPF  = userCreated.CPF 
                    
                }
            );

            var user = await _userService.GetUserByEmailAsync(userCreated.Email);

            Assert.NotNull(user);
            Assert.Equal("Create user register", user.Nome);
        }
    }
}