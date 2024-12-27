using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.Core.Interfaces.User;

using SystemCcharpzinho.Core.Models;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<User?> GetUserByEmailAndPasswordAsync(LoginRequest loginRequest);
    Task<bool> CheckUserByEmailAndCpf(UserCreatedRequest userCreatedRequest);
    Task<User> GetUserByEmailAsync(String email);
}
