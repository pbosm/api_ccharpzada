namespace SystemCcharpzinho.Core.Services.Usuario;

using SystemCcharpzinho.Core.Interfaces.Usuario;
using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Request.Request;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario> GetUserByIdAsync(int id)
    {
        var user = await _usuarioRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado");
        }
        
        return user;
    }

    public async Task<IEnumerable<Usuario>> GetAllUsersAsync()
    {
        return await _usuarioRepository.GetAllUsersAsync();
    }

    public async Task AddUserAsync(Usuario usuario)
    {
        await _usuarioRepository.AddUserAsync(usuario);
    }

    public async Task UpdateUserAsync(int id, UsuarioAtulizadoRequest userRequest)
    {
        var user = await _usuarioRepository.GetUserByIdAsync(id);
        
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

        await _usuarioRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _usuarioRepository.DeleteUserAsync(id);
    }
    
    public async Task<Usuario?> GetUserByEmailAndPassword(LoginRequest loginRequest)
    {
        var user = await _usuarioRepository.GetUserByEmailAndPasswordAsync(loginRequest);
        
        if (user == null)
        {
            throw new ArgumentException("Email ou senha incorretas!");
        }
        
        return user;
    }
    
    public async Task<bool> CheckUserByEmailAndCpf(UsuarioCriadoRequest usuarioCriadoRequest)
    {
        var resultExist = await _usuarioRepository.CheckUserByEmailAndCpf(usuarioCriadoRequest);
        
        return resultExist;
    }
    
    public async Task<Usuario> GetUserByEmailAsync(string email)
    {
        var user = await _usuarioRepository.GetUserByEmailAsync(email);

        if (user == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado");
        }
        
        return user;
    }
}
