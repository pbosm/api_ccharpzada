namespace SystemCcharpzinho.Core.Services.Compra;

using SystemCcharpzinho.Core.Interfaces.Compra;
using SystemCcharpzinho.Core.Models;

public class CompraService : ICompraService
{
    private readonly ICompraRepository _compraRepository;

    public CompraService(ICompraRepository compraRepository)
    {
        _compraRepository = compraRepository;
    }
    
    public async Task<Compra> GetCompraByIdAsync(int id)
    {
        var compra = await _compraRepository.GetCompraByIdAsync(id);

        if (compra == null)
        {
            throw new KeyNotFoundException("Compra não encontrado");
        }
        
        return compra;
    }
}