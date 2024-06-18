using Spectre.Console;

namespace UselessFacts;

public static class PrintUI
{
    public static string PrintOptions(List<string> choices, string title)
    {
        string? select = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .AddChoices(choices)
        );
        return select ?? "";
    }
}