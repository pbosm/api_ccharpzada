using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SystemCcharpzinho.API.Middleware;

public static class EmailPolicy
{
    public static void AddEmailPolicy(this AuthorizationOptions options)
    {
        options.AddPolicy("EmailPolicy", policy =>
            policy.RequireAssertion(context =>
            {
                var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                
                if (userEmail != "emailmockteste@hotmail.com")
                {
                    throw new ErrorReponseMiddleware.ErrorMessage("Esse email não tem permissão para acessar essa API.");
                }

                return true;
            }));
    }
}