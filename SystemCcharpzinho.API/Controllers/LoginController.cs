using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SystemCcharpzinho.API.Dtos;
using SystemCcharpzinho.Core.Interfaces.auth;
using SystemCcharpzinho.Core.Interfaces.Usuario;
using SystemCcharpzinho.Core.Models;
using SystemCcharpzinho.Request.Request;

namespace SystemCcharpzinho.API.Controllers;

[Route("v1/api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUsuarioService  _usuarioService;

    public LoginController(ITokenService tokenService, IUsuarioService usuarioService)
    {
        _usuarioService  = usuarioService;
        _tokenService = tokenService;
    }
    
    [HttpPost("/v1/api/Register")]
    [SwaggerOperation(Summary = "Criando um usuario no sistema.")]
    public async Task<ActionResult> RegisterUser(UsuarioCriadoRequest usuarioCriadoRequest)
    {
        var hasCreated = await _usuarioService.CheckUserByEmailAndCpf(usuarioCriadoRequest);

        if (hasCreated)
        {
            return BadRequest("E-mail ou CPF já cadastrado.");
        }
        
        var user = new Usuario
        {
            Nome  = usuarioCriadoRequest.Nome,
            Senha = usuarioCriadoRequest.Senha,
            Email = usuarioCriadoRequest.Email,
            CPF   = usuarioCriadoRequest.CPF,
        };

        await _usuarioService.AddUserAsync(user);

        return Ok("Usuário criado com sucesso");
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Autentica um usuário e retorna um token JWT e UserDTO.")]
    public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _usuarioService.GetUserByEmailAndPassword(loginRequest);
        
        var token = _tokenService.GenerateToken(user);
        
        var response = new LoginDTO
        {
            Usuario = new LoginDTO.UsuarioResponse
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email
            },
            Token = token
        };

        return Ok(response);
    }
}
