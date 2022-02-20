using Spectre.Console.Cli;
using System.ComponentModel;

namespace PassManager.Console.Commands.Settings
{
    public class AddPasswordSettings : CommandSettings
    {
        [Description("The name of the password entry")]
        [CommandArgument(0, "<Name>")]
        public string Name { get; set; }

        [Description("An additional custom secret")]
        [CommandOption("-s|--secured")]
        public bool ApplyCustomSecret { get; set; }

        [Description("Indicates whether tags will be applied to the entry")]
        [CommandOption("-t|--tags")]
        public bool AddTags { get; set; }
    }
}
