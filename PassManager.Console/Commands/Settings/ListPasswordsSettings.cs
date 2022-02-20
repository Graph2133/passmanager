using Spectre.Console.Cli;
using System.ComponentModel;

namespace PassManager.Console.Commands.Settings
{
    public class ListPasswordsSettings : CommandSettings
    {
        [Description("A query to get password entries")]
        [CommandArgument(0, "[Query]")]
        public string Query { get; set; }

        [Description("Groups all passwords by tags")]
        [CommandOption("-t|--tags")]
        public bool GroupByTags { get; set; }
    }
}
