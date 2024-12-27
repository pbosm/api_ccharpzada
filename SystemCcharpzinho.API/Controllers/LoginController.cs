using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SystemCcharpzinho.API.Dtos;
using SystemCcharpzinho.Core.Interfaces.auth;
using SystemCcharpzinho.Core.Interfaces.User;
using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.API.Controllers;

[Route("v1/api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserService  _userService;

    public LoginController(ITokenService tokenService, IUserService userService)
    {
        _userService  = userService;
        _tokenService = tokenService;
    }
    
    [HttpPost("/v1/api/Register")]
    [SwaggerOperation(Summary = "Criando um usuario no sistema.")]
    public async Task<ActionResult> RegisterUser(UserCreatedRequest userCreatedRequest)
    {
        var hasCreated = await _userService.CheckUserByEmailAndCpf(userCreatedRequest);

        if (hasCreated)
        {
            return BadRequest("E-mail ou CPF já cadastrado.");
        }
        
        var user = new User
        {
            Nome  = userCreatedRequest.Nome,
            Senha = userCreatedRequest.Senha,
            Email = userCreatedRequest.Email,
            CPF   = userCreatedRequest.CPF,
        };

        await _userService.AddUserAsync(user);

        return Ok("Usuário criado com sucesso");
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Autentica um usuário e retorna um token JWT e UserDTO.")]
    public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _userService.GetUserByEmailAndPassword(loginRequest);
        
        var token = _tokenService.GenerateToken(user);
        
        var response = new LoginDTO
        {
            User = new UserDTO(user.Id, user.Nome, user.Email),
            Token = token
        };

        return Ok(response);
    }
}
