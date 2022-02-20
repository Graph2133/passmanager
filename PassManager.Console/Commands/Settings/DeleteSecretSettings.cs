using Spectre.Console.Cli;
using System.ComponentModel;

namespace PassManager.Console.Commands.Settings
{
    public class DeleteSecretSettings : CommandSettings
    {
        [Description("A query to get password entry")]
        [CommandArgument(0, "<Query>")]
        public string Query { get; set; }
    }
}
