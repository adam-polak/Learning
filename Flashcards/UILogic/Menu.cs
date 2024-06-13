using Spectre.Console;

namespace UILogic;

class Menu
{
    Panel p;
    private static List<string> MenuTypes = ["Main", "Add", "Delete", "View Set", "Practice", "View Scores"];
    private static Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>() 
    {
        { "Main", ["Add", "Delete", "View Set", "Practice", "View Scores"] },
        { "Add", ["Add Set", "Add Element To Set", "Back"] },
        { "Add Element To Set", ["Pick", "Back"] },
        { "Delete", ["Delete Set", "Delete Element From Set", "Back"] },
        { "Delete Element From Set", ["Pick", "Back"] },
        { "View Set", ["Pick", "Back"] },
        { "Practice", ["Pick", "Back"] },
        { "View Scores", ["Pick", "Back"] }
    };
    private string lastKey;
    private string curKey;
    private List<string> curList;

    public Menu()
    {
        curKey = MenuTypes.ElementAt(0);
        p = new Panel(curKey);
        List<string>? temp = commands.GetValueOrDefault(curKey);
        curList = temp == null ? new List<string>() : temp;
        lastKey = SetLast();
    }

    public void ExecCommand(int n)
    {
        string command = curList.ElementAt(n);
        switch(command)
        {
            case "Add":
                break;
            case "Delete":
                break;
            case "View Set":
                break;
            case "Practice":
                break;
            case "View Scores":
                break;
            case "Add Set":
                break;
            case "Add Element To Set":
                break;
            case "Delete Set":
                break;
            case "Delete Element From Set":
                break;
            case "Pick":
                break;
            case "Back":
                break;
        }
        Console.WriteLine("Executing command " + n);
    }

    public int GetExitVal()
    {
        if(curKey.Equals(MenuTypes.ElementAt(0))) return curList.Count + 1;
        else return 0;
    }

    public int[] GetCommandRange()
    {
        return [1, curList.Count];
    }

    private string SetLast()
    {
        if(curKey.Equals(MenuTypes.ElementAt(0))) return "";
        else return curKey;
    }
}