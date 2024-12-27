using System.ComponentModel.DataAnnotations;
using SystemCcharpzinho.Request.Helpers;

namespace SystemCcharpzinho.Request.Request;

public class LoginRequest
{   
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
    public string? Email { get; set; }
    
    [MinLength(3, ErrorMessage = "A senha deve ter pelo menos 3 caracteres.")]
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Senha
    {
        get => _senha;
        set => _senha = string.IsNullOrWhiteSpace(value) ? value : CryptoHelper.CryptS(value);
    }
    
    private string _senha;
}