using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.Infrastructure.Repositories.User;

using SystemCcharpzinho.Core.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Infrastructure.Context;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        
        if (user != null)
        {
            _context.Users.Remove(user);
            
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<User?> GetUserByEmailAndPasswordAsync(LoginRequest loginRequest)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Senha == loginRequest.Senha);
        
        return user;
    }
    
    public async Task<bool> CheckUserByEmailAndCpf(UserCreatedRequest userCreatedRequest)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == userCreatedRequest.Email || u.CPF == userCreatedRequest.CPF);
        
        return user != null;
    }
    
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }
}
