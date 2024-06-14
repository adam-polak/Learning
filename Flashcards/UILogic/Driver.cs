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

    public Driver()
    {
        curKey = "Main";
        lastKey = "";
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

    private void ExecCommand(string command)
    {
        List<string> list = new List<string>();
        switch(command)
        {
            case "Add Set":
                foreach(CardStack cardStack in CardStackController.Read(new ValidConnection())) list.Add(cardStack.Name);
                string choice = PrintMenu("Which set would you like to add to?", list);
                break;
            case "Add Element To Set":
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