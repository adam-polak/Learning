using System.Xml.Serialization;
using DataAccessLibrary;
using Spectre.Console;

namespace UILogic;

public class Driver
{
    private static List<string> pickMenuList = ["Pick", "Back"];
    private static Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>() 
    {
        { "Main", ["Add", "Delete", "View Set", "Practice", "View Scores", "Exit"] },
        { "Add", ["Add Set", "Add Element To Set", "Back"] },
        // { "Add Set", pickMenuList },
        // { "Add Element To Set", pickMenuList },
        { "Delete", ["Delete Set", "Delete Element From Set", "Back"] }
        // { "Delete Set", pickMenuList },
        // { "Delete Element From Set", pickMenuList },
        // { "View Set", pickMenuList },
        // { "Practice", pickMenuList },
        // { "View Scores", pickMenuList }
    };
    private string lastKey;
    private string curKey;
    private ValidConnection connection;

    public Driver()
    {
        curKey = "Main";
        lastKey = "";
        connection = new ValidConnection();
    }

    private string PrintMenu()
    {
        return PrintInfo.PrintOptions(curKey, CurList());
    }

    private string PrintMenu(string prompt, List<string> list)
    {
        return PrintInfo.PrintOptions(curKey, prompt, list);
    }

    public void Run()
    {
        string command = "";
        while(!command.Equals("Exit"))
        {
            Console.Clear();
            command = PrintMenu();
            if(command.Equals("Back")) {
                curKey = lastKey;
                lastKey = curKey.Equals("Main") ? "" : "Main";
            } else if(commands.ContainsKey(command)) {
                lastKey = curKey;
                curKey = command;
            } else ExecCommand(command);
            Console.WriteLine("Executing command " + command);
        }
    }

    private List<string> CurList()
    {
        List<string>? temp = commands.GetValueOrDefault(curKey);
        return temp == null ? new List<string>() : temp;
    }

    private string GetUserInput(string? line, List<string> list)
    {
        while(line == null || line.Length == 0 || list.Contains(line))
        {
            if(line == null || line.Length == 0)
            {
                Console.WriteLine("You must enter something...\n\n");
            } else Console.WriteLine("The set already contains \"" + line + "\"...\n\n");
            line = Console.ReadLine();
        }
        return line;
    }

    private string GetUserInput(string? line)
    {
        return GetUserInput(line, new List<string>());
    }

    private void ExecCommand(string command)
    {
        List<string> list = new List<string>();
        string type;
        string userInput;
        string promptMessage;
        switch(command)
        {
            case "Add Set":
                PrintInfo.PrintStackNames(CardStackController.Read(connection));
                promptMessage = "\n\n\n\nEnter what the name of the flashcard stack you want to have will be:\n";
                foreach(CardStack cardStack in CardStackController.Read(connection)) list.Add(cardStack.Name);
                CardStackController.Insert(GetUserInput(Console.ReadLine(), list), connection);
                break;
            case "Add Element To Set":
                foreach(CardStack cardStack in CardStackController.Read(connection)) list.Add(cardStack.Name);
                type = PrintMenu("Which set would you like to add to?", list);
                List<Card> cards = CardController.Read(type, connection);
                List<string> cardFront = new List<string>();
                foreach(Card card in cards) cardFront.Add(card.Front);
                PrintInfo.PrintCards(cards, type);
                promptMessage = "\n\n\n\nEnter what the front of the flashcard you want to add will have:\n";
                Console.WriteLine(promptMessage);
                userInput = GetUserInput(Console.ReadLine(), cardFront);
                Console.Clear();
                PrintInfo.PrintCards(cards, type);
                promptMessage = "\n\n\n\nEnter what the back of \"" + userInput + "\" will have:\n";
                Console.WriteLine(promptMessage);
                Card add = new Card() { Name = type, Front = userInput, Back = GetUserInput(Console.ReadLine()) };
                CardController.Insert(add, connection);
                break;
            case "Delete Set":
                break;
            case "Delete Element From Set":
                break;
            case "View Set":
                break;
            case "Practice":
                break;
            case "View Scores":
                break;
        }
    }
}