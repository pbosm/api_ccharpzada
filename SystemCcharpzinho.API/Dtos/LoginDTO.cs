namespace SystemCcharpzinho.API.Dtos;

public record LoginDTO
{
    public UsuarioResponse Usuario { get; init; }
    public string Token { get; set; }

    public record UsuarioResponse
    {
        public int Id { get; init; }
        public string Nome { get; init; }
        public string Email { get; init; }
    }
}