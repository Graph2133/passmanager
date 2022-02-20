using PassManager.Console.Commands.Settings;
using PassManager.Core.Repository;
using PassManager.Core.VirtualDirectory;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public class ListPasswordsCommand : BasePasswordAsyncCommand<ListPasswordsSettings>
    {
        public ListPasswordsCommand(IPasswordManager passwordManager) : base(passwordManager)
        {
        }

        public override async Task<int> ExecuteAsync(CommandContext context, ListPasswordsSettings settings)
        {
            var entries = await this.GetEntries(settings);
            if (entries.Count == 0)
            {
                AnsiConsole.MarkupLine("No passwords found");
                return 0;
            }


            var root = new Tree("Passwords");
            if (settings.GroupByTags)
            {
                this.AddTagsGroupedByName(root, entries);
            }
            else
            {
                foreach (var entry in entries)
                {
                    root.AddNode(GetEntryMarkup(entry));
                }
            }

            AnsiConsole.Write(root);

            return 0;
        }

        private async Task<List<IndexEntry>> GetEntries(ListPasswordsSettings settings)
        {
            var entries = new List<IndexEntry>();
            if (string.IsNullOrEmpty(settings.Query))
            {
                var passwordEntries = await this.passwordManager.GetAllAsync();
                entries.AddRange(passwordEntries);
            }
            else
            {
                var passwordEntries = await this.passwordManager.GetAllByNameAsync(settings.Query);
                entries.AddRange(passwordEntries);
            }

            return entries;
        }

        private void AddTagsGroupedByName(Tree root, List<IndexEntry> entries)
        {
            var tags = entries.SelectMany(e => e.Tags)
                .Distinct()
                .ToList();

            foreach (var tag in tags)
            {
                var tagNode = root.AddNode($"[darkolivegreen2]{tag}[/]");
                foreach (var entry in entries.Where(e => e.Tags.Contains(tag)))
                {
                    tagNode.AddNode(GetEntryMarkup(entry));
                }
            }

            var entriesWithoutTags = entries.Where(e => e.Tags.Count == 0).ToList();
            if (entriesWithoutTags.Count > 0)
            {
                var tagNode = root.AddNode("NO TAGS:");
                foreach (var entry in entriesWithoutTags)
                {
                    tagNode.AddNode(GetEntryMarkup(entry));
                }
            }
        }

        private string GetEntryMarkup(IndexEntry entry)
        {
            var sb = new StringBuilder(entry.Name);
            if (entry.HasCustomSecret)
            {
                sb.Append(" [darkorange](locked)[/] ");
            }

            return sb.ToString();
        }
    }
}
