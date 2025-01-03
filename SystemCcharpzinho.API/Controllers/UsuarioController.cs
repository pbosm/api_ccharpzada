using Microsoft.AspNetCore.Authorization;
using SystemCcharpzinho.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.API.Controllers;

using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Core.Interfaces.Usuario;

[Route("v1/api/[controller]")]
[ApiController]
[Authorize]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    // [Authorize(Policy = "EmailPolicy")] // Exemplo de uso de policy, onde só vai chamar essa API caso o email seja igual da policy
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Busca e traz um usuario caso exista.")]
    public async Task<ActionResult<UsuarioDTO>> GetUserById(int id)
    {
        var user = await _usuarioService.GetUserByIdAsync(id);
    
        var userDto = new UsuarioDTO(
            user.Id, 
            user.Nome, 
            user.Email, 
            compras: user.Compras?.Count > 0 
                ? user.Compras.Select(compra => new CompraUsuarioDTO(
                    compra.Id, 
                    compra.Product, 
                    compra.Value, 
                    compra.Place, 
                    compra.Date)
                ).ToList()
                : null 
            );
    
        return Ok(userDto);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Busca todos os usuarios.")]
    public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAllUsers()
    {
        var users = await _usuarioService.GetAllUsersAsync();
        
        var userDtos = users.Select(user => new UsuarioDTO(
            user.Id, 
            user.Nome, 
            user.Email, 
            compras: user.Compras?.Count > 0
                ? user.Compras.Select(compra => new CompraUsuarioDTO(
                    compra.Id, 
                    compra.Product, 
                    compra.Value, 
                    compra.Place, 
                    compra.Date)
                ).ToList()
                : null
        ));
    
        return Ok(userDtos);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria um usuario.")]
    public async Task<ActionResult> AddUser(Usuario usuario)
    {
        await _usuarioService.AddUserAsync(usuario);

        return CreatedAtAction(nameof(GetUserById), new { id = usuario.Id }, usuario);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Faz o update de um usuario.")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UsuarioAtulizadoRequest userRequest)
    {
        await _usuarioService.UpdateUserAsync(id, userRequest);

        return Ok();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deleta um usuario.")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await _usuarioService.DeleteUserAsync(id);

        return NoContent();
    }
}