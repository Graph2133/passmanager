using Microsoft.Extensions.DependencyInjection;
using PassManager.Infrastructure.Storage;

namespace PassManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddInfrastructure(this ServiceCollection services)
        {
            services.AddSingleton<IPasswordManagerStorage, PasswordManagerStorage>();
            return services;
        }
    }
}
