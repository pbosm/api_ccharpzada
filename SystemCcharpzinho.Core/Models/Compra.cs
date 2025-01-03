using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemCcharpzinho.Core.Models;

[Table("compras")]
public class Compra
{
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("product")]
    public string Product { get; set; } 

    [Required]
    [Column("value")]
    public decimal Value { get; set; }

    [Required]
    [Column("place")]
    public string Place { get; set; }

    [Required]
    [Column("date")]
    public DateTime Date { get; set; }

    [Required]
    [Column("id_usuario")]
    public int IdUsuario { get; set; } 

    // Relacionamento N:1, muitas compras para um usuário
    [ForeignKey("IdUsuario")]
    public Usuario Usuario { get; set; }
}