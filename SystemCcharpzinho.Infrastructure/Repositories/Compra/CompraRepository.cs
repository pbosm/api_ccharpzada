using Microsoft.EntityFrameworkCore;
using SystemCcharpzinho.Infrastructure.Context;

namespace SystemCcharpzinho.Infrastructure.Repositories.Compra;

using SystemCcharpzinho.Core.Interfaces.Compra;
using SystemCcharpzinho.Core.Models;

public class CompraRepository : ICompraRepository
{
    private readonly AppDbContext _context;

    public CompraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Compra> GetCompraByIdAsync(int id)
    {
        var compra = await _context.Compras
            .Include(c => c.Usuario)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return compra;
    }
}