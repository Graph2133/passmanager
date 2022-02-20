using PassManager.Console.Commands;
using Spectre.Console.Cli;

namespace PassManager.Console.DI
{
    public static class ConfiguratorExtensions
    {
        public static IConfigurator AddAddPasswordCommand(this IConfigurator config)
        {
            config
                .AddCommand<AddPasswordCommand>("add")
                .WithDescription("Adds password entry")
                .WithAlias("a")
                .WithExample(new[] { "add", "example@example.com" })
                .WithExample(new[] { "add", "example@example.com", "-s" })
                .WithExample(new[] { "add", "example@example.com", "--secured" });

            return config;
        }

        public static IConfigurator AddDeletePasswordCommand(this IConfigurator config)
        {
            config
                .AddCommand<DeletePasswordCommand>("delete")
                .WithDescription("Deletes password entry")
                .WithAlias("d")
                .WithExample(new[] { "delete" })
                .WithExample(new[] { "delete", "example@example.com" })
                .WithExample(new[] { "d", "example@example.com" });

            return config;
        }

        public static IConfigurator AddDeleteSecretCommand(this IConfigurator config)
        {
            config
                .AddCommand<DeleteSecretCommand>("d-secret")
                .WithDescription("Deletes custom password secret. Password entry will be encrypted using default app configuration")
                .WithAlias("ds")
                .WithExample(new[] { "d-secret" })
                .WithExample(new[] { "d-secret", "example@example.com" })
                .WithExample(new[] { "ds", "example" });

            return config;
        }

        public static IConfigurator AddUpdatePasswordCommand(this IConfigurator config)
        {
            config
                .AddCommand<UpdatePasswordCommand>("update")
                .WithDescription("Updates password entry")
                .WithAlias("u")
                .WithExample(new[] { "update", "user@example.com" })
                .WithExample(new[] { "u", "example" });

            return config;
        }

        public static IConfigurator AddUpdateSecretCommand(this IConfigurator config)
        {
            config
                .AddCommand<UpdateSecretCommand>("u-secret")
                .WithDescription("Updates custom password secret")
                .WithAlias("us")
                .WithExample(new[] { "u-secret", "user@example.com" })
                .WithExample(new[] { "us", "example" });

            return config;
        }

        public static IConfigurator AddGetPasswordCommand(this IConfigurator config)
        {
            config
                .AddCommand<GetPasswordCommand>("get")
                .WithDescription("Gets password")
                .WithAlias("g")
                .WithExample(new[] { "get", "gmail" })
                .WithExample(new[] { "g", "example@example.com" });

            return config;
        }

        public static IConfigurator AddListPasswordsCommand(this IConfigurator config)
        {
            config
                .AddCommand<ListPasswordsCommand>("list")
                .WithDescription("Lists password entries by provided query")
                .WithAlias("l")
                .WithExample(new[] { "list" })
                .WithExample(new[] { "l", "example" });

            return config;
        }

        public static IConfigurator AddInitializeCommand(this IConfigurator config)
        {
            config
                .AddCommand<InitializeCommand>("init")
                .WithDescription("Initializes application secure storage, index file")
                .WithAlias("i")
                .WithExample(new[] { "init" })
                .WithExample(new[] { "init", "-f" })
                .WithExample(new[] { "i", "-f" });

            return config;
        }

        public static IConfigurator AddPurgeCommand(this IConfigurator config)
        {
            config
                .AddCommand<PurgeCommand>("purge")
                .WithDescription("Deletes all passwords")
                .WithAlias("p")
                .WithExample(new[] { "purge" })
                .WithExample(new[] { "p" });

            return config;
        }

        public static IConfigurator AddGetStatsCommand(this IConfigurator config)
        {
            config
                .AddCommand<GetStatsCommand>("stats")
                .WithDescription("Prints some stats about passwords")
                .WithAlias("st")
                .WithExample(new[] { "stats" })
                .WithExample(new[] { "s" });

            return config;
        }
    }
}
