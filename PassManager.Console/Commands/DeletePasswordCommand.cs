using PassManager.Console.Commands.Settings;
using PassManager.Console.Extensions;
using PassManager.Core.Repository;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public class DeletePasswordCommand : BasePasswordAsyncCommand<DeletePasswordSettings>
    {
        public DeletePasswordCommand(IPasswordManager passwordManager) : base(passwordManager)
        {
        }

        public override async Task<int> ExecuteAsync(CommandContext context, DeletePasswordSettings settings)
        {
            var entry = await this.AskUserToSelectEntry(settings.Query);
            if (entry == null)
            {
                AnsiConsole.MarkupLine($"[red]Provided query is invalid or password does not exist[/]");
                return 0;
            }

            var result = await this.passwordManager.DeletePasswordByNameAsync(entry.Name);

            result.PrintConsoleErrorOrMessage("[green]Password deleted[/]");

            return 0;
        }
    }
}
