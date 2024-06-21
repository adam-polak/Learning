using System.Runtime.InteropServices;
using ShiftLoggerUI.Controllers;
using ShiftLoggerUI.Models;
using Spectre.Console;

namespace ShiftLoggerUI;

public class Driver
{
    private UserController userController;
    private ShiftController shiftController;
    private bool loggedIn;
    private string username;
    private int session_key;

    public Driver()
    {
        userController = new UserController();
        shiftController = new ShiftController();
        loggedIn = false;
        username = "";
    }

    public void LoginScreen()
    {
        if(loggedIn) UserScreen();
        string choice = PrintOptions("What would you like to do?", ["Login", "Create User"]);
        if(choice.Equals("Login")) Login();
        else CreateUser();
    }

    private void UserScreen()
    {
        string choice = PrintOptions("What would you like to do?", ["View Shift History", "Start Shift", "End Shift", "Logout"]);
        switch(choice)
        {
            case "View Shift History":
                List<Shift> shifts = shiftController.ViewShifts(username, session_key);
                PrintShifts(shifts);
                break;
            case "Start Shift":
                Console.WriteLine(shiftController.StartShift(username, session_key));
                break;
            case "End Shift":
                Console.WriteLine(shiftController.EndShift(username, session_key));
                break;
            case "Logout":
                loggedIn = userController.Logout(username, session_key);
                LoginScreen();
                break;
        }
        if(loggedIn) 
        {
            Console.WriteLine("\n(Press enter to continue)");
            Console.ReadLine();
            UserScreen();
        }
    }

    private void Login()
    {
        string user = "";
        string pass;
        string exit;
        int key = -1;
        while(!loggedIn)
        {
            Console.Clear();
            Console.WriteLine("Enter username: ");
            user = Console.ReadLine() ?? "";
            Console.WriteLine("Enter password: ");
            pass = Console.ReadLine() ?? "";
            key = userController.Login(user, pass);
            loggedIn = key != -1;
            if(!loggedIn)
            {
                exit = PrintOptions("Incorrect Login\nWould you like to exit?", ["Yes", "No"]);
                if(exit.Equals("Yes")) break;
            }
        }
        if(!loggedIn) LoginScreen();
        else {
            username = user;
            session_key = key;
            UserScreen();
        }
    }

    private void PrintShifts(List<Shift> shifts)
    {
        Table table = new Table();
        table.Title($"Shifts for {username}");
        table.AddColumn("Shift #");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        foreach(Shift shift in shifts) table.AddRow("" + shift.Id, shift.Start_Time, shift.End_Time);
        AnsiConsole.Write(table);
    }

    private void CreateUser()
    {
        bool valid = false;
        string user;
        string pass;
        while(!valid)
        {
            Console.Clear();
            Console.WriteLine("Enter username: ");
            user = Console.ReadLine() ?? "";
            if(user.Length  < 2)
            {
                Console.WriteLine("Username needs to contain 3 or more characters...");
                Console.WriteLine("(Press enter to continue)");
                Console.ReadLine();
                continue;
            }
            Console.WriteLine("Enter your password: ");
            pass = Console.ReadLine() ?? "";
            if(pass.Length < 6)
            {
                Console.WriteLine("Password needs to contain 6 or more characters...");
                Console.WriteLine("(Press enter to continue)");
                Console.ReadLine();
                continue;   
            }
            valid = userController.CreateUser(user, pass);
            if(!valid)
            {
                Console.WriteLine("Username already exists, use a different one...");
                Console.WriteLine("(Press enter to continue)");
                Console.ReadLine();
                continue;   
            }
        }
        LoginScreen();
    }

    private string PrintOptions(string title, List<string> commands)
    {
        string? select = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(title)
            .AddChoices(commands)
        );
        return select ?? "";
    }
}