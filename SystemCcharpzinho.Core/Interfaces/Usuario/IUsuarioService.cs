namespace SystemCcharpzinho.Core.Interfaces.Usuario;

using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Request.Request;

public interface IUsuarioService
{
    Task<Usuario> GetUserByIdAsync(int id);
    Task<IEnumerable<Usuario>> GetAllUsersAsync();
    Task AddUserAsync(Usuario usuario);
    Task UpdateUserAsync(int id, UsuarioAtulizadoRequest userRequest);
    Task DeleteUserAsync(int id);
    Task<Usuario?> GetUserByEmailAndPassword(LoginRequest loginRequest);
    Task<bool> CheckUserByEmailAndCpf(UsuarioCriadoRequest usuarioCriadoRequest);
    Task<Usuario> GetUserByEmailAsync(String email);
}
