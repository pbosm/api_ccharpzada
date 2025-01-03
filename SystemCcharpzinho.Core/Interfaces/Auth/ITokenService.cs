namespace SystemCcharpzinho.Core.Interfaces.auth;

using SystemCcharpzinho.Core.Models;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
    IDictionary<string, string> DecodeToken(string token);
}