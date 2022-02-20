using Spectre.Console.Cli;
using System.ComponentModel;

namespace PassManager.Console.Commands.Settings
{
    public class GetPasswordSettings : CommandSettings
    {
        [Description("A query to get password entry")]
        [CommandArgument(0, "<Name>")]
        public string Query { get; set; }
    }
}
