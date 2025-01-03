using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.Infrastructure.Repositories.Usuario;

using SystemCcharpzinho.Core.Interfaces.Usuario;
using Microsoft.EntityFrameworkCore;
using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Infrastructure.Context;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> GetUserByIdAsync(int id)
    {
        var user = await _context.Usuarios
            .Include(u => u.Compras)
            .FirstOrDefaultAsync(u => u.Id == id);
        
        return user;
    }

    public async Task<IEnumerable<Usuario>> GetAllUsersAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Compras)
            .ToListAsync();
    }

    public async Task AddUserAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Usuarios.FindAsync(id);
        
        if (user != null)
        {
            _context.Usuarios.Remove(user);
            
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<Usuario?> GetUserByEmailAndPasswordAsync(LoginRequest loginRequest)
    {
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Senha == loginRequest.Senha);
        
        return user;
    }
    
    public async Task<bool> CheckUserByEmailAndCpf(UsuarioCriadoRequest usuarioCriadoRequest)
    {
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == usuarioCriadoRequest.Email || u.CPF == usuarioCriadoRequest.CPF);
        
        return user != null;
    }
    
    public async Task<Usuario> GetUserByEmailAsync(string email)
    {
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }
}
