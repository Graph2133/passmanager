using PassManager.Core.Repository;
using PassManager.Core.VirtualDirectory;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Linq;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public abstract class BasePasswordAsyncCommand<T> : AsyncCommand<T> where T : CommandSettings
    {
        protected readonly IPasswordManager passwordManager;

        protected BasePasswordAsyncCommand(IPasswordManager passwordManager)
        {
            this.passwordManager = passwordManager;
        }

        protected async Task<IndexEntry> AskUserToSelectEntry(string query)
        {
            var entries = await this.passwordManager.GetAllByNameAsync(query);
            if (!entries.Any())
            {
                return null;
            }

            var selectionPrompt = new SelectionPrompt<string>()
                .Title("Select entry:")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more password entries)[/]")
                .AddChoices(entries.Select(e => e.Name).ToArray());
            var selectedEntryName = AnsiConsole.Prompt(selectionPrompt);

            return entries.Single(e => e.Name.Equals(selectedEntryName));
        }
    }

    public abstract class BasePasswordAsyncCommand : AsyncCommand
    {
        protected readonly IPasswordManager passwordManager;

        protected BasePasswordAsyncCommand(IPasswordManager passwordManager)
        {
            this.passwordManager = passwordManager;
        }
    }

}
