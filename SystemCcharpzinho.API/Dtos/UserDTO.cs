using System.Text.Json.Serialization;

namespace SystemCcharpzinho.API.Dtos;

public record UserDTO
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public string Email { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Senha { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CPF { get; init; }

    public UserDTO(int id, string nome, string email, string? senha = null, string? cpf = null)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Senha = senha;
        CPF = cpf;
    }
}
