using System.Text.Json.Serialization;

namespace SystemCcharpzinho.API.Dtos;

public record UsuarioDTO
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public string Email { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Senha { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CPF { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<CompraUsuarioDTO>? Compras { get; init; }

    public UsuarioDTO(int id, string nome, string email, string? senha = null, string? cpf = null, List<CompraUsuarioDTO>? compras = null)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Senha = senha;
        CPF = cpf;
        Compras = compras;
    }
}

public record CompraUsuarioDTO
{
    public int Id { get; init; }
    public string Produto { get; init; }
    public decimal Valor { get; init; }
    public string Lugar { get; init; }
    public DateTime Data { get; init; }

    public CompraUsuarioDTO(int id, string produto, decimal valor, string lugar, DateTime data)
    {
        Id = id;
        Produto = produto;
        Valor = valor;
        Lugar = lugar;
        Data = data;
    }
}
