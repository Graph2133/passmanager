using Spectre.Console.Cli;
using System.ComponentModel;

namespace PassManager.Console.Commands.Settings
{
    public class InitializationSettings : CommandSettings
    {
        [Description("The flag to force deletion of existing passwords and index file")]
        [CommandOption("-f|--force")]
        public bool Force { get; set; }
    }
}
