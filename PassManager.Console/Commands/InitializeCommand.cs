using PassManager.Console.Commands.Settings;
using PassManager.Core.Repository;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public class InitializeCommand : BasePasswordAsyncCommand<InitializationSettings>
    {
        public InitializeCommand(IPasswordManager passwordManager) : base(passwordManager)
        {
        }

        public override async Task<int> ExecuteAsync(CommandContext context, InitializationSettings settings)
        {
            await this.passwordManager.Initialize(settings.Force);

            AnsiConsole.MarkupLine("[green]Application storage intialized.[/]");

            return 0;
        }
    }
}
