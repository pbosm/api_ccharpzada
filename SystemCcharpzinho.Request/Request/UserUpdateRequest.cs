using System.ComponentModel.DataAnnotations;

namespace SystemCcharpzinho.Request.Request;

public class UserUpdateRequest
{
    public string? Nome { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
    public string? Senha { get; set; }

    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve ter 11 dígitos.")]
    public string? CPF { get; set; }
}