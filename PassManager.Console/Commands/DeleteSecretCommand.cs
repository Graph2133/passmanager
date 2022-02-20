using PassManager.Console.Commands.Settings;
using PassManager.Console.Extensions;
using PassManager.Core.Repository;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public class DeleteSecretCommand : BasePasswordAsyncCommand<DeletePasswordSettings>
    {
        public DeleteSecretCommand(IPasswordManager passwordManager) : base(passwordManager)
        {
        }

        public override async Task<int> ExecuteAsync(CommandContext context, DeletePasswordSettings settings)
        {
            var entry = await this.AskUserToSelectEntry(settings.Query);
            if (entry == null)
            {
                AnsiConsole.MarkupLine("[red]Provided query is invalid or password does not exist[/]");
                return 0;
            }

            if (!entry.HasCustomSecret)
            {
                AnsiConsole.MarkupLine("[red]Selected entry is not locked by custom secret[/]");
                return 0;
            }

            var secret = AnsiConsole.Prompt(new TextPrompt<string>("Enter [darkolivegreen2]secret[/] to unlock entry:").Secret());
            var result = await this.passwordManager.DeleteSecret(entry.Name, secret);
            result.PrintConsoleErrorOrMessage("[green]Secret deleted. Password will be encrypted using default configuration[/]");

            return 0;
        }
    }
}
