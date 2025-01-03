using SystemCcharpzinho.Core.Interfaces.Compra;
using SystemCcharpzinho.Core.Interfaces.Usuario;
using SystemCcharpzinho.Infrastructure.Repositories.Compra;
using SystemCcharpzinho.Infrastructure.Repositories.Usuario;

namespace SystemCcharpzinho.API.Extensions
{
    public static class RepositoryRegistrationExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICompraRepository, CompraRepository>();

            return services;
        }
    }
}