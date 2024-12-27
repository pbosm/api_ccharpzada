namespace SystemCcharpzinho.Core.Interfaces.User;

using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Request.Request;

public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task AddUserAsync(User user);
    Task UpdateUserAsync(int id, UserUpdateRequest userRequest);
    Task DeleteUserAsync(int id);
    Task<User?> GetUserByEmailAndPassword(LoginRequest loginRequest);
    Task<bool> CheckUserByEmailAndCpf(UserCreatedRequest userCreatedRequest);
    Task<User> GetUserByEmailAsync(String email);
}
