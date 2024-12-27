namespace SystemCcharpzinho.Core.Services.User;

using SystemCcharpzinho.Core.Interfaces.User;
using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Request.Request;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado");
        }
        
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddUserAsync(user);
    }

    public async Task UpdateUserAsync(int id, UserUpdateRequest userRequest)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        
        if (user == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado");
        }
        
        if (string.IsNullOrEmpty(userRequest.Nome) &&
            string.IsNullOrEmpty(userRequest.Senha) &&
            string.IsNullOrEmpty(userRequest.Email) &&
            string.IsNullOrEmpty(userRequest.CPF))
        {
            throw new ArgumentException("Dados inválidos, favor verificar os campos");
        }

        if (!string.IsNullOrEmpty(userRequest.Nome))
        {
            user.Nome = userRequest.Nome;
        }
        
        if (!string.IsNullOrEmpty(userRequest.Senha))
        {
            user.Senha = userRequest.Senha;
        }
        
        if (!string.IsNullOrEmpty(userRequest.Email))
        {
            user.Email = userRequest.Email;
        }
        
        if (!string.IsNullOrEmpty(userRequest.CPF))
        {
            user.CPF = userRequest.CPF;
        }

        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }
    
    public async Task<User?> GetUserByEmailAndPassword(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetUserByEmailAndPasswordAsync(loginRequest);
        
        if (user == null)
        {
            throw new ArgumentException("Email ou senha incorretas!");
        }
        
        return user;
    }
    
    public async Task<bool> CheckUserByEmailAndCpf(UserCreatedRequest userCreatedRequest)
    {
        var resultExist = await _userRepository.CheckUserByEmailAndCpf(userCreatedRequest);
        
        return resultExist;
    }
    
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        if (user == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado");
        }
        
        return user;
    }
}
