using SystemCcharpzinho.Core.Interfaces.User;
using SystemCcharpzinho.Infrastructure.Repositories.User;

namespace SystemCcharpzinho.API.Utils
{
    public static class RepositoryRegistrationExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}