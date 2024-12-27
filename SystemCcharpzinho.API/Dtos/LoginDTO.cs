using SystemCcharpzinho.Core.Models;

namespace SystemCcharpzinho.API.Dtos;

public record LoginDTO
{
    public UserDTO User { get; set; }
    public string Token { get; set; }
}