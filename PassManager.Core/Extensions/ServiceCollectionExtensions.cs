using PassManager.Core.Index.Read;
using PassManager.Core.Index.Write;
using PassManager.Core.Password.Read;
using PassManager.Core.Password.Write;
using PassManager.Core.Repository;
using PassManager.Infrastructure.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddPassManagerCore(this ServiceCollection services)
        {
            services.AddInfrastructure();

            services
                .AddScoped<IPasswordReader, PasswordReader>()
                .AddScoped<IPasswordWriter, PasswordWriter>();

            services
                .AddScoped<IIndexFileReader, IndexFileReader>()
                .AddScoped<IIndexFileWriter, IndexFileWriter>();

            services.AddScoped<IPasswordManager, PasswordManager>();

            return services;
        }
    }
}
