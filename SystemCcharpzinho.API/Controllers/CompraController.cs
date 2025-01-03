using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SystemCcharpzinho.API.Dtos;
using SystemCcharpzinho.Core.Interfaces.Compra;

namespace SystemCcharpzinho.API.Controllers;

[Route("v1/api/[controller]")]
[ApiController]
[Authorize]
public class CompraController : ControllerBase
{
    private readonly ICompraService _compraService;

    public CompraController(ICompraService compraService)
    {
        _compraService = compraService;
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Busca e traz uma compra caso exista.")]
    public async Task<ActionResult<CompraDTO>> GetCompraById(int id)
    {
        var compra = await _compraService.GetCompraByIdAsync(id);

        UsuarioCompraDTO? usuarioCompraDto = null;
        
        if (compra.Usuario != null)
        {
            usuarioCompraDto = new UsuarioCompraDTO(
                compra.Usuario.Id,
                compra.Usuario.Nome,
                compra.Usuario.Email
            );
        }

        var compraDto = new CompraDTO(
            compra.Id,
            compra.Product,
            compra.Value,
            compra.Place,
            compra.Date,
            usuarioCompraDto
        );

        return Ok(compraDto);
    }
}