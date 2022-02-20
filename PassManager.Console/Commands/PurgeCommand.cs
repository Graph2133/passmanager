using PassManager.Core.Repository;
using Spectre.Console;
using Spectre.Console.Cli;

namespace PassManager.Console.Commands
{
    public class PurgeCommand : BasePasswordCommand
    {
        public PurgeCommand(IPasswordManager passwordManager) : base(passwordManager)
        {
        }

        public override int Execute(CommandContext context)
        {
            if (AnsiConsole.Confirm("Purge all passwords ?"))
            {
                this.passwordManager.PurgeAll();
                AnsiConsole.MarkupLine("[green]Passwords deleted[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Operation completed. Passwords were not deleted[/]");
            }

            return 0;
        }
    }
}
