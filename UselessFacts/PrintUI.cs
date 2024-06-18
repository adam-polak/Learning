using Spectre.Console;
using UselessFacts.Models;

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

    public static void PrintFact(Fact fact, string factType)
    {
        Table table = new Table();
        table.Title = new TableTitle(factType);
        table.AddColumn("Source");
        table.AddColumn("Fact");
        table.AddRow(fact.source_url, fact.text);
        AnsiConsole.Write(table);
        Console.WriteLine("\n\nPress enter to return...");
        Console.ReadLine();
    }
}