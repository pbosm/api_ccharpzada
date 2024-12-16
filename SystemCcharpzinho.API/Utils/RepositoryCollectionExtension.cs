using SystemCcharpzinho.Core.Interfaces;
using SystemCcharpzinho.Infrastructure.Repositories;

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