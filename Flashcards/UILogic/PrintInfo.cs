using DataAccessLibrary;
using Spectre.Console;

namespace UILogic;

static class PrintInfo
{
    public static string PrintOptions(string title, List<string> commands)
    {
        return PrintOptions(title, "What would you like to do?", commands);
    }

    public static string PrintOptions(string title, string prompt, List<string> commands)
    {
        prompt = "\n" + prompt;
        int length = prompt.Length - 1;
        int center = (length / 2) - (title.Length / 2);
        while(length > 0)
        {
            prompt = "-" + prompt;
            length--;
        }
        prompt = title + "\n" + prompt;
        while(center > 0)
        {
            prompt = " " + prompt;
            center--;
        }
        string? select = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(prompt)
                .AddChoices(commands)
        );
        return select == null ? "" : select;
    }

    public static void PrintStackNames(List<CardStack> cardStacks)
    {
        if(cardStacks.Count == 0)
        {
            Console.WriteLine("***There are no flashcard sets yet***");
            return;
        }
        Table table = new Table();
        table.Title("Flashcard Sets");
        table.AddColumn("Name");
        foreach(CardStack cardStack in cardStacks) table.AddRow(cardStack.Name);
        AnsiConsole.Write(table);
    }

    public static void PrintCards(List<Card> cards, string type)
    {
        if(cards.Count == 0) 
        {
            Console.WriteLine("***There are no cards yet***");
            return;
        }
        Table table = new Table();
        table.Title(type);
        table.AddColumn("ID");
        table.AddColumn("Front");
        table.AddColumn("Back");
        foreach(Card card in cards) table.AddRow("" + card.Id, card.Front, card.Back);
        AnsiConsole.Write(table);
    }
}