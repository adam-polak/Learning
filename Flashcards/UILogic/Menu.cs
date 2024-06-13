using Spectre.Console;

namespace UILogic;

class Menu
{
    Panel p;
    private static List<string> MenuTypes = ["Main", "Add", "Delete", "View Set", "Practice", "View Scores"];
    private static Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>() 
    {
        { "Main", ["Add", "Delete", "View Set", "Practice", "View Scores"] },
        { "Add", ["Add Set", "Add Element To Set"] },
        { "Add Element To Set", ["Pick"] },
        { "Delete", ["Delete Set", "Delete Element From Set"] },
        { "Delete Element From Set", ["Pick"] },
        { "View Set", ["Pick"] },
        { "Practice", ["Pick"] },
        { "View Scores", ["Pick"] }
    };
    private string curKey;
    private List<string> curList;

    public Menu()
    {
        curKey = MenuTypes.ElementAt(0);
        p = new Panel(curKey);
        List<string>? temp = commands.GetValueOrDefault(curKey);
        curList = temp == null ? new List<string>() : temp;
    }

    public void ExecCommand(int n)
    {
        Console.WriteLine("Executing command " + n);
    }

    public int[] GetCommandRange()
    {
        return [1, curList.Count];
    }
}