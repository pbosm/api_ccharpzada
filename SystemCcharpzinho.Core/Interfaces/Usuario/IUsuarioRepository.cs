using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.Core.Interfaces.Usuario;

using SystemCcharpzinho.Core.Models;

public interface IUsuarioRepository
{
    Task<Usuario> GetUserByIdAsync(int id);
    Task<IEnumerable<Usuario>> GetAllUsersAsync();
    Task AddUserAsync(Usuario usuario);
    Task UpdateUserAsync(Usuario usuario);
    Task DeleteUserAsync(int id);
    Task<Usuario?> GetUserByEmailAndPasswordAsync(LoginRequest loginRequest);
    Task<bool> CheckUserByEmailAndCpf(UsuarioCriadoRequest usuarioCriadoRequest);
    Task<Usuario> GetUserByEmailAsync(String email);
}
