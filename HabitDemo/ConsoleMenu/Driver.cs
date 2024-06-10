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
        while(run) {
            DisplayMainMenu();
            int reply = GetReply(Console.ReadLine());
            while(reply == 0) {
                Console.Clear();
                DisplayMainMenu();
                InvalidAnswer(commands.Count() + 1);
                reply = GetReply(Console.ReadLine());
            }
            MainMenuOperators(reply);
        }
        ClosingOperation();
    }

    private void MainMenuOperators(int x) {
        if(x == commands.Count() + 1) run = false;
        else RunCommand(commands.ElementAt(x - 1));
    }

    private void DisplayInputMenu() {
        PrintDash();
        Console.WriteLine("Enter how many glasses of water you would like to record for today: ");
        PrintDash();
    }

    private void DisplayUpdateMenu(int largest) {
        PrintDash();
        Console.WriteLine("Enter which (#)day you would like to update (Days [1-" + largest + "]): ");
        PrintDash();
    }

    private void DisplayDeleteMenu(int largest) {
        PrintDash();
        Console.WriteLine("Enter which (#)day you would like to delete (Days [1-" + largest + "]): ");
        PrintDash();
    }

    private void NoDaysTracked() {
        Console.WriteLine("There are no days tracked yet....\n\n\n\n\nPress enter to continue...");
        string? wait = Console.ReadLine();
    }

    private void RunCommand(string s) {
        int reply = 0;
        int largestDay = connection.GetLargestDay();
        switch(s) {
            case "Input Habit":
                Console.Clear();
                DisplayInputMenu();
                reply = GetReply(Console.ReadLine());
                while(reply == 0) {
                    Console.Clear();
                    DisplayInputMenu();
                    PrintDash();
                    Console.WriteLine("Invalid entry. (Enter an integer)");
                    PrintDash();
                    reply = GetReply(Console.ReadLine());
                }
                connection.InsertWaterEntry(reply);
                break;
            case "Update Habit":
                Console.Clear();
                if(largestDay == 0) {
                    NoDaysTracked();
                    break;
                }
                DisplayUpdateMenu(largestDay);
                reply = GetReply(Console.ReadLine());
                while(reply == 0 || reply > largestDay) {
                    Console.Clear();
                    DisplayUpdateMenu(largestDay);
                    InvalidAnswer(largestDay);
                    reply = GetReply(Console.ReadLine());
                }
                int day = reply;
                Console.Clear();
                Console.WriteLine("Enter the new amount for day " + day + ": ");
                reply = GetReply(Console.ReadLine());
                while(reply == 0) {
                    Console.Clear();
                    Console.WriteLine("Enter the new amount for day " + reply + ": ");
                    PrintDash();
                    Console.WriteLine("Invalid entry. (Enter an integer)");
                    PrintDash();
                    reply = GetReply(Console.ReadLine());
                }
                connection.UpdateByDay(day, reply);
                break;
            case "Delete Habit":
                Console.Clear();
                if(largestDay == 0) {
                    NoDaysTracked();
                    break;
                }
                DisplayDeleteMenu(largestDay);
                reply = GetReply(Console.ReadLine());
                while(reply == 0 || reply > largestDay) {
                    Console.Clear();
                    DisplayDeleteMenu(largestDay);
                    InvalidAnswer(largestDay);
                    reply = GetReply(Console.ReadLine());
                }
                connection.DeleteDay(reply);
                break;
            case "View Habit History":
                break;
        }
    }

    private int GetReply(string? r) {
        if(r == null || r.Length == 0) return 0;
        char[] arr = r.ToCharArray();
        int ans = 0;
        int raise = 0;
        for(int i = arr.Length - 1; i >= 0; i--) {
            if(arr[i] >= '0' && arr[i] <= '9') ans += (arr[i] - '0') * (int)Math.Pow(10, raise++);
            else return 0;
        }
        return ans;
    }

    private void ClosingOperation() {
        Console.Clear();
        connection.Close();
    }

    private void PrintDash() {
        Console.WriteLine("---------------------------------------------------------------");
    }

    private void InvalidAnswer(int end) {
        PrintDash();
        Console.WriteLine("Invalid entry. (Enter a number 1-" + end + ")");
        PrintDash();
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