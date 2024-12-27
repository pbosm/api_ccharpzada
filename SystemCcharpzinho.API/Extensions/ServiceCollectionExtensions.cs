using SystemCcharpzinho.Core.Interfaces.auth;
using SystemCcharpzinho.Core.Interfaces.User;
using SystemCcharpzinho.Core.Services.Auth;
using SystemCcharpzinho.Core.Services.User;

namespace SystemCcharpzinho.API.Utils

{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            
            return services;
        }
    }
}