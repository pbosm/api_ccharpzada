namespace SystemCcharpzinho.Core.Interfaces.auth;

using SystemCcharpzinho.Core.Models;

public interface ITokenService
{
    string GenerateToken(User user);
    IDictionary<string, string> DecodeToken(string token);
}