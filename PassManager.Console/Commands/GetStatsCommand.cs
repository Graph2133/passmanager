using PassManager.Core.Repository;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassManager.Console.Commands
{
    public class GetStatsCommand : BasePasswordAsyncCommand
    {
        public GetStatsCommand(IPasswordManager passwordManager) : base(passwordManager)
        {
        }

        public override async Task<int> ExecuteAsync(CommandContext context)
        {
            var entries = await this.passwordManager.GetAllAsync();
            AnsiConsole.MarkupLine($"Total number of passwords: [green]{entries.Count}[/]");

            var tagsFrequency = new Dictionary<string, int>();
            foreach (var entry in entries)
            {
                foreach (var tag in entry.Tags)
                {
                    if (tagsFrequency.ContainsKey(tag))
                    {
                        tagsFrequency[tag]++;
                    }
                    else
                    {
                        tagsFrequency.Add(tag, 1);
                    }
                }
            }


            if (entries.Count == 0)
            {
                return 0;
            }

            var barChart = new BarChart()
                .Width(80)
                .Label("Passwords by tags:");

            var passwordsWithoutTagsCount = entries.Where(e => e.Tags.Count == 0).Count();

            if (passwordsWithoutTagsCount > 0)
            {
                barChart.AddItem("NO TAGS:", passwordsWithoutTagsCount, Color.DarkOliveGreen2);
            }

            if (tagsFrequency.Count > 0)
            {
                barChart.AddItems(
                    tagsFrequency,
                    (item) => new BarChartItem(item.Key, item.Value, Color.DarkOliveGreen2));
            }

            AnsiConsole.Write(barChart);

            return 0;
        }
    }
}
