using PassManager.Console.Clipboard;
using PassManager.Console.Commands.Settings;
using PassManager.Console.Extensions;
using PassManager.Core.Repository;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public class GetPasswordCommand : BasePasswordAsyncCommand<DeletePasswordSettings>
    {
        public GetPasswordCommand(IPasswordManager passwordManager) : base(passwordManager)
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

            string customSecret = null;
            if (entry.HasCustomSecret)
            {
                var customSecretPrompt = new TextPrompt<string>("Enter [darkolivegreen2]secret[/]:").Secret();
                customSecret = AnsiConsole.Prompt(customSecretPrompt);
            }

            var result = await this.passwordManager.GetPasswordAsync(entry.Name, customSecret);
            if (result.Success)
            {
                WindowsClipboard.SetText(result.ExtensionData);
            }

            result.PrintConsoleErrorOrMessage("[green]Password copied to buffer[/]");

            return 0;
        }
    }
}
