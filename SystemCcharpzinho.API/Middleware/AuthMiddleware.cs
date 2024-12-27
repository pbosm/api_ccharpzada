using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var authorizeAttributes = endpoint?.Metadata.GetOrderedMetadata<AuthorizeAttribute>();

        if (authorizeAttributes != null && authorizeAttributes.Any())
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                
                try
                {
                    var jwtToken = handler.ReadJwtToken(token);
                    
                    if (jwtToken.ValidTo > DateTime.UtcNow)
                    {
                        context.User = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims));
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        
                        await context.Response.WriteAsync("Token expired");
                        
                        return;
                    }
                }
                catch
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    
                    await context.Response.WriteAsync("Invalid token");
                    
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                
                await context.Response.WriteAsync("Unauthorized");
                
                return;
            }
        }

        await _next(context);
    }
}