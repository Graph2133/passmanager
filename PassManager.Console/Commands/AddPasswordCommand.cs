using PassManager.Console.Commands.Settings;
using PassManager.Console.Extensions;
using PassManager.Core.Repository;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public sealed class AddPasswordCommand : BasePasswordAsyncCommand<AddPasswordSettings>
    {
        public AddPasswordCommand(IPasswordManager passwordManager) : base(passwordManager)
        {
        }

        public override async Task<int> ExecuteAsync(CommandContext context, AddPasswordSettings settings)
        {
            var password = this.AskPassword();
            var customSecret = this.AskCustomSecret(settings);
            var tags = this.AskTags(settings);

            var result = await this.passwordManager.CreatePasswordAsync(settings.Name, password, customSecret, tags);
            result.PrintConsoleErrorOrMessage($"[green]Password for {settings.Name} saved[/]");

            return 0;
        }

        private string AskPassword()
        {
            var passwordPrompt = new TextPrompt<string>("Enter [darkolivegreen2]password[/]:")
                .Secret()
                .Validate(v => !string.IsNullOrEmpty(v), "Password must be provided");

            return AnsiConsole.Prompt(passwordPrompt);
        }

        private string AskCustomSecret(AddPasswordSettings settings)
        {
            string customSecret = null;
            if (settings.ApplyCustomSecret)
            {
                var customSecretPrompt = new TextPrompt<string>("Enter [darkolivegreen2]custom secret[/]:")
                    .Secret()
                    .Validate(v => !string.IsNullOrEmpty(v), "Secret value cannot be empty");

                customSecret = AnsiConsole.Prompt(customSecretPrompt);
            }
            return customSecret;
        }

        private List<string> AskTags(AddPasswordSettings settings)
        {
            var tags = new List<string>();
            if (settings.AddTags)
            {
                AnsiConsole.MarkupLine("Enter tags one by one or leave value as empty and press enter to continue.");
                var tagValuePrompt = new TextPrompt<string>(
                        "Enter [darkolivegreen2]tag value[/]:")
                    .AllowEmpty()
                    .PromptStyle("red");

                while (true)
                {
                    var tagValue = AnsiConsole.Prompt(tagValuePrompt);
                    if (string.IsNullOrEmpty(tagValue))
                    {
                        break;
                    }

                    tags.Add(tagValue);
                }
            }

            return tags;
        }
    }
}
