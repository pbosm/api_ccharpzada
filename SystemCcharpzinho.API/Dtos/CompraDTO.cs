namespace SystemCcharpzinho.API.Dtos;

public record CompraDTO
{
    public int Id { get; init; }
    
    public string Produto { get; init; }
    
    public decimal Valor { get; init; }
    
    public string Lugar { get; init; }
    
    public DateTime Data { get; init; }
    
    public UsuarioCompraDTO Usuario { get; set; }
    
    public CompraDTO(int id, string product, decimal value, string place, DateTime date, UsuarioCompraDTO usuarioCompra)
    {
        Id = id;
        Produto = product;
        Valor = value;
        Lugar = place;
        Data = date;
        Usuario = usuarioCompra;
    }
}

public record UsuarioCompraDTO
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public string Email { get; init; }

    public UsuarioCompraDTO(int id, string nome, string email)
    {
        Id = id;
        Nome = nome;
        Email = email;
    }
}