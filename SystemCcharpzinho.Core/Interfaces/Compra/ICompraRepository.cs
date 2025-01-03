namespace SystemCcharpzinho.Core.Interfaces.Compra;

using SystemCcharpzinho.Core.Models;

public interface ICompraRepository
{
    Task<Compra> GetCompraByIdAsync(int id);
}