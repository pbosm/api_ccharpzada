using System.ComponentModel.DataAnnotations;
using SystemCcharpzinho.Request.Helpers;

namespace SystemCcharpzinho.Request.Request;

public class UserCreatedRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email é inválido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
    public string Senha
    {
        get => _senha;
        set => _senha = string.IsNullOrWhiteSpace(value) ? value : CryptoHelper.CryptS(value);
    }
    private string _senha;

    [Required(ErrorMessage = "CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve ter 11 dígitos.")]
    public string? CPF { get; set; }
}