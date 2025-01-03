using SystemCcharpzinho.Core.Interfaces.auth;
using SystemCcharpzinho.Core.Interfaces.Compra;
using SystemCcharpzinho.Core.Interfaces.Usuario;
using SystemCcharpzinho.Core.Services.Auth;
using SystemCcharpzinho.Core.Services.Compra;
using SystemCcharpzinho.Core.Services.Usuario;

namespace SystemCcharpzinho.API.Extensions

{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICompraService, CompraService>();
            services.AddScoped<ITokenService, TokenService>();
            
            return services;
        }
    }
}