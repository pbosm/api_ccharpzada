using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemCcharpzinho.Core.Models;

[Table("usuarios")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("nome")]
    public string? Nome { get; set; }
    
    [Required]
    [EmailAddress]
    [Column("email")]
    public string? Email { get; set; }
    
    [Required]
    [Column("senha")]
    public string? Senha { get; set; }
    
    [Required]
    [Column("cpf")]
    public string? CPF { get; set; }
}