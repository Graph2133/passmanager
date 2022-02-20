using Microsoft.Extensions.DependencyInjection;
using PassManager.Console.DI;
using Spectre.Console.Cli;

namespace PassManager.Console
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPassManagerCore();
            var registrar = new TypeRegistrar(serviceCollection);
            var app = new CommandApp(registrar);

            app.Configure(config =>
            {
                // management
                config
                    .AddInitializeCommand()
                    .AddPurgeCommand();

                // password commands
                config
                    .AddAddPasswordCommand()
                    .AddDeletePasswordCommand()
                    .AddUpdatePasswordCommand()
                    .AddGetPasswordCommand()
                    .AddListPasswordsCommand();

                // secret commands
                config
                    .AddDeleteSecretCommand()
                    .AddUpdateSecretCommand();

                config.AddGetStatsCommand();
            });

            return app.Run(args);
        }
    }
}
