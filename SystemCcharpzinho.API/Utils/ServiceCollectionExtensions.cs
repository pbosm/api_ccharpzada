using SystemCcharpzinho.Core.Services;

namespace SystemCcharpzinho.API.Utils
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<UserService>();

            return services;
        }
    }
}