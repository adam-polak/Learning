using DataAccessLibrary;

namespace ConsoleMenu;

public class Driver {

    private Connection connection;
    private bool run;
    private List<string> commands = ["Input Habit", "Update Habit", "Delete Habit", "View Habit History"];
    

    public Driver() {
        connection = new Connection();
        run = true;
    }

    public void Run() {
        DisplayMainMenu();
    }

    private void PrintDash() {
        Console.WriteLine("--------------------------------------------------");
    }

    private void DisplayMainMenu() {
        Console.Clear();
        int end = commands.Count() + 1;
        PrintDash();
        Console.WriteLine("What would you like to do? (Enter a number 1-" + end + ")");
        PrintDash();
        int listNum = 1;
        foreach(string command in commands) Console.WriteLine(listNum++ + ". " + command);
        PrintDash();
        Console.WriteLine(end + ". Exit");
        PrintDash();
    }

}