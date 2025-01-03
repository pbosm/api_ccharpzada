namespace SystemCcharpzinho.Core.Interfaces.Compra;

using SystemCcharpzinho.Core.Models;

public interface ICompraService
{
    Task<Compra> GetCompraByIdAsync(int id);
}