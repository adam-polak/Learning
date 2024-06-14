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
}