using PassManager.Console.Commands.Settings;
using PassManager.Console.Extensions;
using PassManager.Core.Repository;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public class UpdatePasswordCommand : BasePasswordAsyncCommand<UpdatePasswordSettings>
    {
        public UpdatePasswordCommand(IPasswordManager passwordManager) : base(passwordManager)
        {
        }

        public override async Task<int> ExecuteAsync(CommandContext context, UpdatePasswordSettings settings)
        {
            var entry = await this.AskUserToSelectEntry(settings.Query);
            if (entry == null)
            {
                AnsiConsole.MarkupLine($"[red]Provided query is invalid or password does not exist[/]");
                return 0;
            }

            var updatedName = AnsiConsole.Prompt(
                new TextPrompt<string>(
                    "[grey][[Optional. Press enter to skip.]][/] Updated [darkolivegreen2]name[/]:")
                .AllowEmpty());

            var updatedPassword = AnsiConsole.Prompt(
                new TextPrompt<string>(
                    "[grey][[Optional. Press enter to skip.]][/] Updated [darkolivegreen2]password[/]:")
                .Secret()
                .AllowEmpty());

            var result = await this.passwordManager.UpdatePasswordByNameAsync(
                entry.Name, updatedName, updatedPassword);

            result.PrintConsoleErrorOrMessage("[green]Password updated[/]");

            return 0;
        }
    }
}
