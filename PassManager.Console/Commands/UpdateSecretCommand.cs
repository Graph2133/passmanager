using PassManager.Console.Commands.Settings;
using PassManager.Console.Extensions;
using PassManager.Core.Repository;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public class UpdateSecretCommand : BasePasswordAsyncCommand<UpdateSecretSettings>
    {
        public UpdateSecretCommand(IPasswordManager passwordManager) : base(passwordManager)
        {
        }

        public override async Task<int> ExecuteAsync(CommandContext context, UpdateSecretSettings settings)
        {
            var entry = await this.AskUserToSelectEntry(settings.Query);
            if (entry == null)
            {
                AnsiConsole.MarkupLine($"[red]Provided query is invalid or password does not exist[/]");
                return 0;
            }

            if (!entry.HasCustomSecret)
            {
                AnsiConsole.MarkupLine("[red]Selected entry is not locked by custom secret[/]");
                return 0;
            }

            var oldSecret = AnsiConsole.Prompt(
                new TextPrompt<string>(
                    "Old [darkolivegreen2]secret[/]:")
                .Secret());

            var newSecret = AnsiConsole.Prompt(
                new TextPrompt<string>(
                    "New [darkolivegreen2]secret[/]:")
                .Secret());

            var result = await this.passwordManager.UpdateSecret(entry.Name, oldSecret, newSecret);

            result.PrintConsoleErrorOrMessage("[green]Secret updated[/]");

            return 0;
        }
    }
}
