using PassManager.Shared;
using Spectre.Console;

namespace PassManager.Console.Extensions
{
    public static class ResultExtensions
    {
        public static void PrintConsoleErrorOrMessage<T>(this BaseResult<T> result, string message) where T : BaseResult<T>
        {
            if (result.Success)
            {
                AnsiConsole.MarkupLine(message);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{result.ErrorMessage}[/]");
            }
        }
    }
}
