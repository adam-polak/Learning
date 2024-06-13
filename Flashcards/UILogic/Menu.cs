using Spectre.Console;

namespace UILogic;

class Menu
{

    Panel p;
    private static List<string> pickMenuList = ["Pick", "Back"];
    private static Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>() 
    {
        { "Main", ["Add", "Delete", "View Set", "Practice", "View Scores"] },
        { "Add", ["Add Set", "Add Element To Set", "Back"] },
        { "Add Set", pickMenuList },
        { "Add Element To Set", pickMenuList },
        { "Delete", ["Delete Set", "Delete Element From Set", "Back"] },
        { "Delete Set", pickMenuList },
        { "Delete Element From Set", pickMenuList },
        { "View Set", pickMenuList },
        { "Practice", pickMenuList },
        { "View Scores", pickMenuList }
    };
    private string lastKey;
    private string curKey;

    public Menu()
    {
        curKey = "Main";
        p = new Panel(curKey);
        lastKey = "";
    }

    public void ExecCommand(int n)
    {
        string command = CurList().ElementAt(n);
        if(command.Equals("Back")) {
            curKey = lastKey;
            lastKey = "Main";
        } else if(command.Equals("Pick")) {
            lastKey = "";
            Pick();
        } else {
            lastKey = curKey;
            curKey = command;
        }
        Console.WriteLine("Executing command " + n);
    }

    public int GetExitVal()
    {
        if(curKey.Equals("Main")) return CurList().Count + 1;
        else return 0;
    }

    public int[] GetCommandRange()
    {
        return [1, CurList().Count];
    }

    private List<string> CurList()
    {
        List<string>? temp = commands.GetValueOrDefault(curKey);
        return temp == null ? new List<string>() : temp;
    }

    private void Pick()
    {
        switch(curKey)
        {
            case "Add Set":
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