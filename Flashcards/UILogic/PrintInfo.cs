using Spectre.Console;

namespace UILogic;

static class PrintInfo
{
    public static string PrintOptions(string title, List<string> commands)
    {
        string? select = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title + "\nWhat would you like to do?")
                .AddChoices(commands)
        );
        return select == null ? "" : select;
    }
}