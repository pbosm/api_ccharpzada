using Microsoft.AspNetCore.Authorization;
using SystemCcharpzinho.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.API.Controllers;

using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Core.Interfaces.User;

[Route("v1/api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // [Authorize(Policy = "EmailPolicy")] // Exemplo de uso de policy, onde só vai chamar essa API caso o email seja igual da policy
    [Authorize]
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Busca e traz um usuario caso exista.")]
    public async Task<ActionResult<UserDTO>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
    
        var userDto = new UserDTO(user.Id, user.Nome, user.Email);
    
        return Ok(userDto);
    }

    [Authorize]
    [HttpGet]
    [SwaggerOperation(Summary = "Busca todos os usuarios.")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        
        var userDtos = users.Select(user => new UserDTO(user.Id, user.Nome, user.Email));
    
        return Ok(userDtos);
    }

    [Authorize]
    [HttpPost]
    [SwaggerOperation(Summary = "Cria um usuario.")]
    public async Task<ActionResult> AddUser(User user)
    {
        await _userService.AddUserAsync(user);

        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [Authorize]
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Faz o update de um usuario.")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UserUpdateRequest userRequest)
    {
        await _userService.UpdateUserAsync(id, userRequest);

        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deleta um usuario.")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);

        return NoContent();
    }
}