using Spectre.Console.Cli;
using System.ComponentModel;

namespace PassManager.Console.Commands.Settings
{
    public class UpdatePasswordSettings : CommandSettings
    {
        [Description("A query to get password entry")]
        [CommandArgument(0, "<Query>")]
        public string Query { get; set; }
    }
}
